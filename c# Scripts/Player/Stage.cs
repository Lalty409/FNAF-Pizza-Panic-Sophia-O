using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public bool isPerforming;
    private GameManager gameManager;
    private HealthManager PlayerHealth;
    public Color DangerColor;
    public Color NormalColor;
    private SpriteRenderer Bar;

    private void Start()
    {
        Bar = GameObject.Find("TimeLeftBar_Bar_Stage").GetComponent<SpriteRenderer>();
        PlayerHealth = GameObject.Find("Health").GetComponent<HealthManager>();
        gameManager = GameObject.Find("EvilGameManager").GetComponent<GameManager>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") & !isPerforming)
        {
            isPerforming = true;
            //Debug.Log("Hor hor hor hor hor hor hor hor hor hor");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") & isPerforming)
        {
            isPerforming = false;
            //Debug.Log("No more hor");
        }
    }
    public void startStageHealthBar()
    {
        StartCoroutine(StageHealthBar());
    }

    private IEnumerator StageHealthBar()
    {
        Debug.Log("running");
        float fixedVal = 0;
        int kidTemp;
        int maxTemp;

        while (gameManager.getDaytime())
        {
            kidTemp = gameManager.getDancingKidCount();
            maxTemp = gameManager.getDancingKidMax();

            fixedVal = 1 - (float)Mathf.Clamp(kidTemp, 0, maxTemp) / maxTemp;
            
            Bar.color = (NormalColor * fixedVal) + (DangerColor * (1 - fixedVal));
            Bar.size = new Vector2(fixedVal, 0.5f);
            if (kidTemp > maxTemp)
            {
                PlayerHealth.onDamage();
            }

            yield return new WaitUntil(() => !gameManager.isPaused);
        }
        
    }
}
