using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAnim : MonoBehaviour
{
    [SerializeField]
    private Sprite[] wolfAnimSprits;

    [SerializeField]
    private float animTimeTreshold = 0.15f;

    private WolfAI wolfAI;

    private SpriteRenderer sr;

    private int state = 0;
    private float animTimer;


    private void Awake()
    {
        wolfAI = GetComponent<WolfAI>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (wolfAI.isMoving)
        {
            if (Time.time > animTimer)
            {
                sr.sprite = wolfAnimSprits[state % wolfAnimSprits.Length];
                state++;
                if (state == wolfAnimSprits.Length)
                    state = 0;

                animTimer = Time.time + animTimeTreshold;
            }
        }
        else
            sr.sprite = wolfAnimSprits[0];

        sr.flipX = wolfAI.left;

    }




}//class



