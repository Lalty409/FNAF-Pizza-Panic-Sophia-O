using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayEndScreen : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject confetti;
    private Clock clock;

    void Start()
    {
        winScreen.SetActive(false);
        clock = FindObjectOfType<Clock>();
        assignEvent();
        confetti.SetActive(false);
        
    }

    private void assignEvent()
    {
            clock.OnDayEnd += DisplayWinScreen;
    }





    private void DisplayWinScreen()
    {
        winScreen.SetActive(true);
        Animator winimator = winScreen.GetComponent<Animator>();
        winimator.SetTrigger("Win");
        confetti.SetActive(true);

    }
}