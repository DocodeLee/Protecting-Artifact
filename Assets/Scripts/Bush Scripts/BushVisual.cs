using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushVisual : MonoBehaviour
{
    public enum BushVariant {Green, Cyan, Yellow}

    private BushVariant bushVariant;

    private void Start()
    {
        bushVariant = BushVariant.Green;

        int bushVariantIndex = (int)BushVariant.Green;

    }



}// class
