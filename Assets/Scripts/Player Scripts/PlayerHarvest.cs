using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarvest : MonoBehaviour
{
    [SerializeField]
    private float harvestTime = 0.4f;

    private PlayerMovement playerMovement;
    private PlayerBackpack backapck;

    private AudioSource audioSource;

    private Collider2D collidedBush;
    private BushFruits hitBushs;

    private bool canHarvestFrutis;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        backapck = GetComponent<PlayerBackpack>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            TryHarvestFruit();
        }
    }

    void TryHarvestFruit()
    {
        if (!canHarvestFrutis)
            return;
        if(collidedBush != null)
        {
            hitBushs = collidedBush.GetComponent<BushFruits>();

            if(hitBushs.HasFruits())
            {
                audioSource.Play();
                playerMovement.HarvestStopMovement(harvestTime);
                backapck.AddFruits(hitBushs.HarvestFruit());
            }    
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bush"))
        {
            canHarvestFrutis = true;
            collidedBush = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bush"))
        {
            canHarvestFrutis = false;
            collidedBush = null;
        }
    }


}// class
