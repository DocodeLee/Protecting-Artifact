using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushFruits : MonoBehaviour
{
    [SerializeField]
    private int[] amountPerType;

    [SerializeField]
    private float[] respawnTime;

    private BushVisual bushVisual;

    private bool hasFruits;
    private float timer;

    private void Awake()
    {
        bushVisual = GetComponent<BushVisual>();

        //randomly initialize some bushs and fruits
        if (Random.Range(0, 2) == 0)
        { 
            hasFruits = false;
            timer = Time.time + respawnTime[(int)bushVisual.GetBushVaraint()];

        }
        else
        {
            hasFruits = true;
            bushVisual.ShowFruits();
        }
    }

    private void Update()
    {
        if (Time.time > timer)
        {
            hasFruits = true;
            bushVisual.ShowFruits();
        }
    }
    public int HarvestFruit()
    {
        if (hasFruits)
        {
            hasFruits = false;
            bushVisual.HideFruits();
            timer = Time.time + respawnTime[(int)bushVisual.GetBushVaraint()];
            return amountPerType[(int)bushVisual.GetBushVaraint()];
        }
        else
            return 0;
    }

    public bool HasFruits()
    {
        return hasFruits;
    }

    //when enemy attacts the bush and eats it
    public void EatBushFruits()
    {
        enabled = false;
        bushVisual.SetToDry();
    }



}//class
