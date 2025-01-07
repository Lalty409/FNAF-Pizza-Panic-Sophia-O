using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int currDay;
    public static int DifficultyLvl;
    public bool isPaused;
    public bool isPlayingDaytime;
    public tableManager tm;
    public Kid ki;
    private int numActiveTables;
    private int kidSpawnSpeed;
    public GameObject GameoverScreenIMG;

    public int DancingKidCounter;
    public int DancingKidMax;

    public static GameObject Bonnie;
    public static GameObject Player;
    public static GameObject DialogueBox;

    void Start()
    {
        DancingKidMax = 20;
        Bonnie = GameObject.Find("Bonnie");
        Player = GameObject.FindGameObjectWithTag("Player");
        DialogueBox = GameObject.Find("Text");
        DifficultyLvl = 1;
        currDay = 0;
        Introduction();

        
        isPlayingDaytime = true;
        StartCoroutine(NewKid());
        StartCoroutine(DaytimeCoroutine(DifficultyLvl));
    }


    
    public static IEnumerator MoveTo(Transform Object, Vector3 ObjDestination, float SecondsToMove)
    {
        float t = 0f;
        Vector3 ObjStart = Object.position;
        float maxDistanceDelta = Vector3.Distance(ObjStart, ObjDestination);
        while (t < SecondsToMove)
        {
            t += Time.deltaTime;
            Object.position = Vector3.MoveTowards(ObjStart,
                                                  ObjDestination,
                                                  maxDistanceDelta * (t / SecondsToMove) );
            
            yield return null;
        }
        Object.position = ObjDestination;
    }
    Coroutine WaitedMoveTo(Transform Object, Vector3 ObjDestination, float SecondsToMove)
    {
        return StartCoroutine(MoveTo(Object, ObjDestination, SecondsToMove));
    }
    static public IEnumerator WaitThenExecute(float Seconds, Action MyFunction )
    {
        yield return new WaitForSeconds(Seconds);
        MyFunction();
    }


    IEnumerator IntroductionCoroutine()
    {
        isPaused = true;
        Typewriter typewriter = DialogueBox.GetComponent<Typewriter>();
        yield return WaitedMoveTo(Bonnie.transform,
                                  new Vector3(3.62f, 14.88f, 0),
                                  3);
        yield return typewriter.TypeWriterManual("Tutorial", 0, 1, false);
        yield return typewriter.TypeWriterManual("Tutorial", 1, 1, false);
        yield return typewriter.TypeWriterManual("Tutorial", 2, 1, true);
        typewriter.turnOffDiaBox();
        Player.GetComponent<Movement>().facing = true;
        StartCoroutine(MoveTo(Player.transform,
                                  new Vector3(4.5f, 12f, 0f),
                                  2));
        yield return WaitedMoveTo(Bonnie.transform,
                                  new Vector3(4.5f, 12f, 0f),
                                  2);
        Player.transform.position = new Vector3(4.5f, 3.5f, 0);
        Bonnie.transform.position = new Vector3(4.5f, 3.5f, 0);

        StartCoroutine(MoveTo(Player.transform,
                                  new Vector3(3f, -0.6f, 0),
                                  2f));
        yield return WaitedMoveTo(Bonnie.transform,
                                  new Vector3(3.9f, 0.25f, 0),
                                  1.8f);



        yield return null;
        Bonnie.transform.position = new Vector3(10.45f,15f,0f);
    }


    IEnumerator DaytimeCoroutine(int diff)
    {
        GameObject.Find("Stagebox").GetComponent<Stage>().startStageHealthBar();
        numActiveTables = 4; //diff*2;
        kidSpawnSpeed = 25-diff*2;

        tm.setActiveTables(numActiveTables);

        while (isPlayingDaytime && !isPaused)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(kidSpawnSpeed/3, kidSpawnSpeed));
            StartCoroutine(NewKid());
        }
        yield return null;
    }

    IEnumerator NewKid()
    {
        GameObject newChild = tm.SpawnChild(numActiveTables); //spawn kid at selected chair pos

        yield return new WaitForSeconds(3);
        if (newChild != null)
        {
            ki = newChild.GetComponent<Kid>(); 
            ki.takeOrder(newChild.transform.GetSiblingIndex()); 
        }

        yield return null;
    }
    void Introduction()
    {
        Bonnie.transform.position = new Vector3(10.45f, 15f, 0f);
        //StartCoroutine(IntroductionCoroutine());
    }
    public void Gameover()
    {
        GameoverScreenIMG.SetActive(true);
        Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        cam.transform.position = GameObject.Find("Lose").transform.position;
        isPaused = true;

        Player.GetComponent<Movement>().setDialogueVal(true);
        GameoverScreen();
    }
    private Coroutine GameoverScreen()
    {
        return StartCoroutine(GameoverScreenCoroutine());
    }
    IEnumerator GameoverScreenCoroutine()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("trys again");
                removeGameover();
                break;
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("Quits");
                break;
            }
            yield return null;
        }
        removeGameover();
    }
    private void removeGameover()
    {
        Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        cam.transform.position = GameObject.Find("MainRoomCam").transform.position;
        Player.GetComponent<Movement>().setDialogueVal(false);
        GameoverScreenIMG.SetActive(true);
    }
    public void AddDancingKidCount(int addVal)
    {
        DancingKidCounter += addVal;
    }
    public int getDancingKidCount()
    {
        return DancingKidCounter;
    }
    public int getDancingKidMax()
    {
        return DancingKidMax;
    }
    public bool getDaytime()
    {
        return isPlayingDaytime;
    }
}

