using System.Collections;
using UnityEngine;

public class DancingKid : MonoBehaviour
{
    public Stage st;
    public Sprite[] dKidSprites;
    private float singVal;
    private bool isMoving;
    private SpriteRenderer spriteRenderer;
    private Vector3 initPos;
    private GameObject stagebox;
    private GameManager GM;

    public void MakeDancingKid(Vector3 ip, int spriteVal)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = dKidSprites[spriteVal];

        singVal = 10;
        isMoving = false;
        initPos = ip;
        StartCoroutine(EnterRoom(initPos));
    }

    private void Awake()
    {
        stagebox = GameObject.FindGameObjectWithTag("Stage");
        st = stagebox.GetComponent<Stage>();
        GM = GameObject.Find("EvilGameManager").GetComponent<GameManager>();
    }


    private void Update()
    {
        if (st.isPerforming && !isMoving && spriteRenderer!=null)
        {
            singVal -= Time.deltaTime;


            if (Mathf.Sin(singVal*8)>0) { spriteRenderer.flipX = true; }
            else { spriteRenderer.flipX = false; }
            
            if (singVal <= 0)
            {
                GM.AddDancingKidCount(-1);
                StartCoroutine(ExitRoom());
            }
        }
    }


    

    private IEnumerator EnterRoom(Vector3 initialPos)
    {   
        //set initial position of kid (make it stand up)
        transform.position = initialPos;
        isMoving = true;
        //move the kid from table to room
        KidMovement movement = GetComponent<KidMovement>();
        yield return movement.MoveTo(new Vector3(3f,17.6f,0));
        yield return movement.MoveTo(new Vector3(3f, 25.5f, 0));

        //move to position at stageroom
        float randx = Random.Range(-1.8f,7.8f);
        yield return movement.MoveTo(new Vector3(randx, 28.8f, 0)); 
        isMoving = false;
        GM.AddDancingKidCount(1);
    }

    private IEnumerator ExitRoom()
    {
        isMoving = true;
        //move the kid out of the room
        KidMovement movement = GetComponent<KidMovement>();
        yield return movement.MoveTo(new Vector3(3f, 25.5f, 0)); 
        Destroy(gameObject);
    }
}