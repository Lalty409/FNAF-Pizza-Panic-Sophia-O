using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Typewriter : MonoBehaviour
{
    private DialogWriter dialogWriter;
    private Movement PlayerMovement;
    private Animator ChatboxAnimation;
    private GameObject Player;
    public bool paused = false;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerMovement = Player.GetComponent<Movement>();

        dialogWriter = gameObject.GetComponent<DialogWriter>();
        ChatboxAnimation = gameObject.GetComponentInParent<Animator>();

        gameObject.GetComponent<MeshRenderer>().sortingOrder = 5;
        gameObject.GetComponentInParent<SpriteRenderer>().sortingOrder = 5;
    }
        

    IEnumerator TypeWrite(string[] Text, int SecondsTilFinished, bool WaitForUserInput, bool TurnOffDiaBox)
    {
        PlayerMovement.setDialogueVal(true);
        //.Replace(@"\n", "\n")
        dialogWriter.Text.text = string.Empty;
        yield return new WaitForSeconds(0.5f);
        float t = 0;
        int CharIndex = 0;
        for (int i = 0; i < Text.Length; i++)
        {
            ChatboxAnimation.SetBool("InDialogue", true);
            t = 0;
            
            CharIndex = 0;
            Text[i] = Text[i].Replace(@"\n", "\n");
            while (t < SecondsTilFinished)
            {
                ChatboxAnimation.SetBool("InDialogue", !paused);
                if (!paused)
                {
                    PlayerMovement.InDialogue = true;
                    t += Time.deltaTime;
                    CharIndex = Mathf.FloorToInt(
                         Mathf.Clamp( (t / SecondsTilFinished) * (Text[i].Length-1),0, Text[i].Length-1)
                    );

                    dialogWriter.Text.text = Text[i].Substring(0, CharIndex);
                }
                else
                {
                    dialogWriter.Text.text = string.Empty;
                    PlayerMovement.InDialogue = false;
                }
                yield return null;
            }
        }
        yield return new WaitForSeconds(1f);
        if (WaitForUserInput)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        }
        dialogWriter.Text.text = string.Empty;
        ChatboxAnimation.SetBool("InDialogue", TurnOffDiaBox);
        PlayerMovement.setDialogueVal(false);

    }
    public void TypeWriter(string DialogueName)
    {
        Debug.Log(PlayerMovement);
        DialogueObj dialogue = getAssetDialogueObj(DialogueName);

        StartCoroutine(TypeWrite(dialogue.Dialogue, dialogue.SecondsToWrite, true, true));
    }
    public Coroutine TypeWriterManual(string DialogueName,int DialogIndex, int SecondsToWrite, bool WaitForUserInput)
    {
        Debug.Log(PlayerMovement);
        DialogueObj dialogue = getAssetDialogueObj(DialogueName);
        string[] temp = new string[1];
        temp[0] = dialogue.Dialogue[DialogIndex];

        return StartCoroutine(TypeWrite(temp, SecondsToWrite, WaitForUserInput, false));
    }
    private DialogueObj getAssetDialogueObj(string DialogueName)
    {
        string[] DialogFolder = { "Assets/Dialogue" };
        string path = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets(DialogueName, DialogFolder)[0]);
        return AssetDatabase.LoadAssetAtPath<DialogueObj>(path);
    }
    public void turnOffDiaBox()
    {
        ChatboxAnimation.SetBool("InDialogue", false);
    }

}
