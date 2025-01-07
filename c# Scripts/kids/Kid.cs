using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public Sprite[] orderSprites;
    public GameObject DancingKidPrefab;

    private int orderVal;
    public int spriteVal;
    private Transform orderBubble;
    private float OrderTimeCountdown;
    private Coroutine OrderTimeCoroutine;

    private Transform TimerBar;
    private HealthManager PlayerHealth;
    public Color DangerColor;
    public Color NormalColor;
    private GameManager GM;

    private void Start()
    {
        PlayerHealth = GameObject.Find("Health").GetComponent<HealthManager>();
        GM = GameObject.Find("EvilGameManager").GetComponent<GameManager>();
    }

    public void takeOrder(int posVal)
    {
        if (posVal % 2 == 0) // left
        {
            orderBubble = gameObject.transform.Find("orderLeft");
            TimerBar = gameObject.transform.Find("TimeLeftBar_Empty_Left");
        }
        else // right
        {
            orderBubble = gameObject.transform.Find("orderRight");
            TimerBar = gameObject.transform.Find("TimeLeftBar_Empty_Right");
        }
        SpriteRenderer spriteRenderer = orderBubble.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = orderSprites[orderVal];
        orderBubble.gameObject.SetActive(true);
        TimerBar.gameObject.SetActive(true);

        orderVal = UnityEngine.Random.Range(0, 5);
        switch (orderVal)
        {
            case 0: // drink
                spriteRenderer.sprite = orderSprites[0]; break;
            case 1: // cheesepez
                spriteRenderer.sprite = orderSprites[1]; break;
            case 2: // meatpez
                spriteRenderer.sprite = orderSprites[2]; break;
            case 3: // veggpez
                spriteRenderer.sprite = orderSprites[3]; break;
            case 4: // combopez
                spriteRenderer.sprite = orderSprites[4]; break;
            case 5: // pinepez
                spriteRenderer.sprite = orderSprites[5]; break;
            default:
                break;
        }
        OrderTimeStart(30);
    }

    public int GetOrderVal()
    {
        return orderVal;
    }

    public void CompleteOrder()
    {
        StopOrderTime();
        orderBubble.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            Projectile projectileObj = collision.GetComponent<Projectile>();
            if (projectileObj != null && projectileObj.getOrderVal() == orderVal)
            {
                Destroy(projectileObj.gameObject);
                CompleteOrder();
            }
        }
    }

    public void OrderTimeStart(int SecondsTilStrike)
    {
        OrderTimeCountdown = SecondsTilStrike;
        OrderTimeCoroutine = StartCoroutine(OrderCountdownCoroutine());

    }

    public IEnumerator OrderCountdownCoroutine()
    {
        float BeginTime = OrderTimeCountdown;
        SpriteRenderer TimeBar = TimerBar.GetChild(0).GetComponent<SpriteRenderer>();
        float normalizedProgression = 1;
        
        while (OrderTimeCountdown > 0)
        {
            OrderTimeCountdown -= Time.deltaTime;
            normalizedProgression = (OrderTimeCountdown / BeginTime);
            TimeBar.size = new Vector2((Mathf.FloorToInt(normalizedProgression * 30) + 1) / 30.0f, 0.5f);
            TimeBar.color = (NormalColor * normalizedProgression) + (DangerColor * (1-normalizedProgression));
            yield return new WaitUntil(() => !GM.isPaused);
        }
        OrderTimeCountdown = 0;
        Debug.Log("you died lol / lost a life");
        PlayerHealth.onDamage();
        resetChair();
    }
    public void StopOrderTime()
    {
        StopCoroutine(OrderTimeCoroutine);
        resetChair();
    }
    private void resetChair()
    {
        orderBubble.gameObject.SetActive(false);
        TimerBar.gameObject.SetActive(false);
        TimerBar.GetChild(0).GetComponent<SpriteRenderer>().color = NormalColor;
        gameObject.SetActive(false);
        OrderTimeCountdown = 0;

        //for stage mechanic
        GameObject newDKid = Instantiate(DancingKidPrefab, transform.position, Quaternion.identity);
        DancingKid dkComponent = newDKid.GetComponent<DancingKid>();
        dkComponent.MakeDancingKid(transform.position, spriteVal);
        Debug.Log("Making Dancing Kid");
    }
}