using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 direction;
    public SpriteRenderer spriteRenderer; 
    public Sprite[] sprites; 
    public Collider2D col;
    private int sprVal;

    public void MakeProjectile(Vector2 dir, int spVal)
    {      
        this.sprVal = spVal-10;
        spriteRenderer.sprite = sprites[spVal-10];
        direction = dir;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D hitted) 
    {
        // any object with the destryProj tag 
        if (hitted.CompareTag("DestroyProjOnContact"))
        {
            Destroy(gameObject);
        }
    }
    

    public int getOrderVal()
    {
        return sprVal;
    }
}