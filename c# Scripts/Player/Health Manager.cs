using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class HealthManager : MonoBehaviour
{
    public int LivesLeft;
    private SpriteRenderer Hearts;
    private SpriteRenderer EmptyHearts;
    private Vector2 OriginalSize;
    private Coroutine CurrentBounce;
    private GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        LivesLeft = 3;

        GM = GameObject.Find("EvilGameManager").GetComponent<GameManager>();
        Hearts = gameObject.GetComponent<SpriteRenderer>();
        EmptyHearts = gameObject.GetComponentInChildren<SpriteRenderer>();
        OriginalSize = Hearts.size;
        updateHearts();
    }

    public void onDamage()
    {
        LivesLeft--;
        updateHearts();
    }
    public void onHeal()
    {
        LivesLeft++;
        updateHearts();
    }
    private void updateHearts()
    {
        if (LivesLeft > 0)
        {
            Vector2 temp = new Vector2(LivesLeft * OriginalSize.x, OriginalSize.y);
            Hearts.size = temp;
            EmptyHearts.size = temp;
            CurrentBounce = BounceHearts(0.5f, 1f);
        }
        else
        {
            GM.Gameover();
        }

    }
    private Coroutine BounceHearts(float secsToReturn, float distance)
    {
        if(CurrentBounce != null)
        {
            StopCoroutine(CurrentBounce);
        }
        return StartCoroutine(BounceHeartsCoroutine(secsToReturn, distance));
    }
    private IEnumerator BounceHeartsCoroutine(float secsToReturn, float distance)
    {
        Vector3 OriginalPosition = gameObject.transform.localPosition;
        float t = secsToReturn;
        float temp = 0;
        Vector3 temp2 = new Vector3();
        while (t > 0)
        {
            temp = (t / secsToReturn);
            temp2 = new Vector3(OriginalPosition.x, 
                                OriginalPosition.y - distance * (1 - Mathf.Sqrt(1 - temp * temp)),
                                OriginalPosition.z);
            Hearts.transform.localPosition = temp2;
            EmptyHearts.transform.localPosition = temp2;

            t -= Time.deltaTime;
            yield return null;
        }
    }
}
// X^2 + Y^2 = 1
// 1 - X^2 = Y^2
// sqrt(1 - X^2) = y
