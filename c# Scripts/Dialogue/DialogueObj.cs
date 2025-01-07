using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Object", menuName = "Dialogue/DialogueObject", order = 1)]
public class DialogueObj : ScriptableObject
{
    public string[] Dialogue;
    public int SecondsToWrite = 6;
}
