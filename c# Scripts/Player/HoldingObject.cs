using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst;
using UnityEngine;

public class HoldingObject : MonoBehaviour
{
    public Animator controller;
    private Animator Foodcontroller;
    private Transform FoodObj;
    private Movement PlayerMovement;

    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector2 holdingPosition;
        public Vector2 ProjectileDirection;
        public int projectileSpriteVal; 
    }

    public int holdingVal;
        /*0=nothing
         * 1=dough
         * 2=flatdough
         * 3=saucydough
         * 4=saucycheesydough
         * 5=meatdough
         * 6=veggiedough
         * 7=meatveggiedough
         * 8=pineappledough
         * 9=crappypizza
         * 10=drink
         * 11=cheesepizza
         * 12=meatpizza
         * 13=veggiepizza
         * 14=meatveggiepizza
         * 15=pineapplepizza
         */

    void Start()
    {
        PlayerMovement = gameObject.GetComponent<Movement>();
        controller = GetComponent<Animator>();
        FoodObj = gameObject.transform.Find("ThingHeld");
        Foodcontroller = FoodObj.GetComponent<Animator>();
    }

    void Update()
    {
        if (holdingVal > 0)
        {
            controller.SetBool("HoldingItem", true); 
            Foodcontroller.SetFloat("FoodItem", holdingVal / 16f);
            FoodObj.gameObject.SetActive(true);
        }
        else
            controller.SetBool("HoldingItem", false);


        Foodcontroller.SetFloat("FoodItem", holdingVal / 16f);

        if (Input.GetKeyDown(KeyCode.Space) & !PlayerMovement.getDialogueVal())
        {
            if (holdingVal >= 10)
            {
                Vector2 dirc = controller.GetBool("Facing") ? Vector2.right : Vector2.left;
                Vector2 postn = gameObject.transform.position;
                OnShoot?.Invoke(this, new OnShootEventArgs { 
                    holdingPosition = postn, ProjectileDirection = dirc, projectileSpriteVal = holdingVal});
            }
        }
    }

    public void setHoldingItem(int ItemID)
    {
        holdingVal = ItemID;
    }
}