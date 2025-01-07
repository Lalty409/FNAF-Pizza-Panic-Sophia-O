using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tableManager : MonoBehaviour
{
    public GameObject[] allTables;
    public Sprite[] childSprites;
    private int randKid;

    public GameObject SpawnChild(int activeTables)
    {
        if (IsMaxCapacity())
        {
            return null;
        }
        int i = 0;
        while (i!=-1)
        {
            i = UnityEngine.Random.Range(0, activeTables);
            Transform childLeft = allTables[i].transform.Find("childLeft");
            Transform childRight = allTables[i].transform.Find("childRight");

            if (!childLeft.gameObject.activeSelf)
            {
                randKid = Random.Range(0, childSprites.Length);

                SpriteRenderer spriteRenderer = childLeft.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = childSprites[randKid];
                childLeft.GetComponent<Kid>().spriteVal = randKid;

                childLeft.gameObject.SetActive(true);
                return childLeft.gameObject; // Return the activated child object
            }

            if (!childRight.gameObject.activeSelf)
            {
                randKid = Random.Range(0, childSprites.Length);

                SpriteRenderer spriteRenderer = childRight.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = childSprites[randKid];
                childRight.GetComponent<Kid>().spriteVal = randKid;
                spriteRenderer.flipX = true;

                childRight.gameObject.SetActive(true);
                return childRight.gameObject; // Return the activated child object
            }
        }
        return null;
    }

    public void setActiveTables(int activeTables) //to be called once every round begin
    {
        foreach (var table in allTables)
        {
            table.SetActive(false);
        }
        for (int j = 0; j < activeTables; j++)
        {
            allTables[j].SetActive(true);
        }

    }

    public bool IsMaxCapacity()
    {
        bool full = true;
        for (int i = 0; i < allTables.Length; i++)
        {
            full = full & allTables[i].transform.Find("childLeft").gameObject.activeSelf;
            full = full & allTables[i].transform.Find("childRight").gameObject.activeSelf;
        }
        return full;
    }
}
