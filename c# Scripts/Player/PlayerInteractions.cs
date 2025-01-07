using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    enum Items
    {
        Nothing,
        Dough,
        SaucyPizzaDough,
        CheeseySaucyPizzaDough,
        PeperoniPizzaDough,
        VeggiePizzaDough,
        PeperoniVeggiePizzaDough,
        PineapplePizzaDough,
        Crap,
        Soda,
        CheeseySaucyPizza,
        PeperoniVeggiePizza,
        PeperoniPizza,
        VeggiePizza,
        PineapplePizza,
    }
    public GameObject Player;
    private HoldingObject objectHeld;
    private void Reset()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        objectHeld = Player.GetComponent<HoldingObject>();
    }

    public void onDoughBox()
    {
        if (objectHeld.holdingVal == 0)
        {
            objectHeld.holdingVal = 1;
        }
    }
    public void onDoughRollingStation()
    {
        if (objectHeld.holdingVal == 1)
        {
            objectHeld.holdingVal = 2;
        }
    }
    public void onTomatoSauce()
    {
        switch (objectHeld.holdingVal)
        {
            case 2:
                objectHeld.holdingVal = 3;
                break;

            case 0:
                break;
            default:
                objectHeld.holdingVal = 9;
                break;
        }
    }
    public void onCheese()
    {
        switch (objectHeld.holdingVal)
        {
            case 3:
                objectHeld.holdingVal = 4;
                break;

            case 0:
                break;
            default:
                objectHeld.holdingVal = 9;
                break;
        }
    }
    public void onPeperoni()
    {
        switch (objectHeld.holdingVal)
        {
            case 4:
                objectHeld.holdingVal = 5;
                break;
            case 6:
                objectHeld.holdingVal = 7;
                break;

            case 0:
                break;
            default:
                objectHeld.holdingVal = 9;
                break;
        }
    }
    public void onVeggie()
    {
        switch (objectHeld.holdingVal)
        {
            case 4:
                objectHeld.holdingVal = 6;
                break;
            case 5:
                objectHeld.holdingVal = 7;
                break;

            case 0:
                break;
            default:
                objectHeld.holdingVal = 9;
                break;
        }
    }
    public void onOven()
    {
        if (objectHeld.holdingVal > 10)
        {
            return;
        }
        switch (objectHeld.holdingVal)
        {
            case 4:
                objectHeld.holdingVal = 11;
                break;
            case 5:
                objectHeld.holdingVal = 12;
                break;
            case 6:
                objectHeld.holdingVal = 13;
                break;
            case 7:
                objectHeld.holdingVal = 14;
                break;
            case 8:
                objectHeld.holdingVal = 15;
                break;

            case 0:
                break;
            default:
                objectHeld.holdingVal = 9;
                break;
        }
    }

    public void onVendo()
    {
        if (objectHeld.holdingVal == 0)
        {
            objectHeld.holdingVal = 10;
        }
    }

}
