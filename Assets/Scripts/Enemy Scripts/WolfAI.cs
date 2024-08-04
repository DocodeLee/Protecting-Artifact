using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAI : MonoBehaviour
{

    [SerializeField]
    private bool isEater;

    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField]
    private int attackDamage = 5;

    [SerializeField]
    private float attackTimeTreshold = 1f;

    [SerializeField]
    private float eatTimeTreshold = 2f;

    [SerializeField]
    private LayerMask bushMask;

    [HideInInspector]
    public bool isMoving, left;

    private Artifact artifact;

    private BushFruits bushFrutisTarget;

    private float attackTimer;
    private float eatTimer;

    private bool killingBush;
    private bool isAttacking;


    // Start is called before the first frame update
    void Start()
    {
        if (isEater)
        {
            //SearchForTarget();
            killingBush = false;
        }
        else
        {
            isAttacking = false;
        }
        artifact = GameObject.FindWithTag("Artifact").GetComponent<Artifact>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!artifact)
            return;
        if (isEater)
        {
            if (bushFrutisTarget && bushFrutisTarget.enabled &&
                bushFrutisTarget.HasFruits() && !killingBush)
            {
                // if not close to the bush continue walking toward, else stop and eat the bush
                if (Vector2.Distance(transform.position, bushFrutisTarget.transform.position) > 0.5f)
                {
                    float step = moveSpeed * Time.deltaTime;

                    transform.position = Vector2.MoveTowards(transform.position,
                        bushFrutisTarget.transform.position, step);

                    isMoving = true;
                }
                else
                {
                    isMoving = false;
                    bushFrutisTarget.HarvestFruit();
                    eatTimer = Time.time + eatTimeTreshold;
                    killingBush = true;
                }
            }
            else if (killingBush)
            {
                if (Time.time > eatTimer)
                {
                    bushFrutisTarget.EatBushFruits();
                    killingBush = false;

                    SearchForTarget();
                }
            }
            else
            {
                SearchForTarget();
            }
            if(bushFrutisTarget)
            {
                if (bushFrutisTarget.transform.position.x < transform.position.x)
                    left = true;
                else
                    left = false;
            }

            if (!bushFrutisTarget)
                SearchForTarget();

        }// if isEater
        else
        {
            //wolf taht destroys artifact
            if (Vector2.Distance(transform.position, artifact.transform.position) > 1.5f)
            {
                float step = moveSpeed * Time.deltaTime;

                transform.position = Vector2.MoveTowards(transform.position,
                    artifact.transform.position, step);

                isMoving = true;
            }
            else if (!isAttacking)
            {
                isAttacking = true;
                attackTimer = Time.time + attackTimeTreshold;

                isMoving = false;

            }
            if (isAttacking)
            {
                if(Time.time > attackTimer)
                {
                    Attack();
                    attackTimer = Time.time + attackTimeTreshold;

                }
            }

            if (artifact.transform.position.x < transform.position.x)
                left = true;
            else
                left = false;
        }

    } //update

    void SearchForTarget()
    {
        bushFrutisTarget = null;
        
        Collider2D[] hits;

        for (int i = 1; i < 50; i++)
        {
            hits = Physics2D.OverlapCircleAll(transform.position, Mathf.Exp(i), bushMask);

            foreach (Collider2D hit in hits)
            {
                if(hit && (hit.GetComponent<BushFruits>().HasFruits() &&
                    hit.GetComponent<BushFruits>().enabled))
                {
                    bushFrutisTarget = hit.GetComponent<BushFruits>();
                    break;
                }
            }
            if (bushFrutisTarget)
                break;

        }
    
    } // searchForTarget()

    void Attack()
    {
        artifact.TakeDamage(attackDamage);
    }

}//class
