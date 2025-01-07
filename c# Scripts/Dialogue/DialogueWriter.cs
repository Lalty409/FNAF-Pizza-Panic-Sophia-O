using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogWriter : MonoBehaviour
{
    public TextMeshPro Text;
    // Start is called before the first frame update
    
    public void Write(string text)
    {
        Text.text = text;
    }

}
