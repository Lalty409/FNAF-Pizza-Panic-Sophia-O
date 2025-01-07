using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private DialogWriter clockWriter;
    public SpriteRenderer spriteRenderer;
    public Sprite[] clockFaces;
    public event Action OnDayEnd;
    public GameManager GM;
    private GameManager gameManager;

    public string TimeDisplay;
    public static DateTime inGameTime;
    private float elapsedTime;

    void Start()
    {
        gameManager = GameObject.Find("EvilGameManager").GetComponent<GameManager>();
        clockWriter = gameObject.GetComponent<DialogWriter>();
        gameObject.GetComponent<MeshRenderer>().sortingOrder = 5;
        GM = FindAnyObjectByType<GameManager>();

        inGameTime = new DateTime(1, 1, 1, 10, 0, 0);// Initialize in-game time to 10:00 AM
        spriteRenderer.sprite = clockFaces[0];

        elapsedTime = 0f;
        UpdateTimeDisplay();
    }

    void Update()
    {
        if(!gameManager.isPaused)
            elapsedTime += Time.deltaTime;

        if (elapsedTime >= 20f) //1hr in-game = 20sec
        {
            elapsedTime -= 20f; // Reset elapsed time
            inGameTime = inGameTime.AddHours(1); 


            if (inGameTime.Hour >= 22)
            {
                Debug.Log("its 10pm");
                OnDayEnd?.Invoke(); //day end event here <----------------- !!!!!!
                GM.isPlayingDaytime = false;
            }   

                if (inGameTime.Hour == 10) {
                spriteRenderer.sprite = clockFaces[0];
            }
            if (inGameTime.Hour == 13) {
                spriteRenderer.sprite = clockFaces[1];
            }
            if (inGameTime.Hour == 16) {
                spriteRenderer.sprite = clockFaces[2];
            }
            if (inGameTime.Hour == 19) {
                spriteRenderer.sprite = clockFaces[3];
            }

            UpdateTimeDisplay();
        }

    }
    void UpdateTimeDisplay()
    {
        // Format the in-game time as a string and display it
        clockWriter.Text.text = inGameTime.ToString("hh:mm\n  tt");
    }
}

