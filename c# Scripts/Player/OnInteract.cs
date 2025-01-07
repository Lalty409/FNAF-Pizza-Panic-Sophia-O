using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] public GameObject Player;
    [SerializeField] public UnityEvent Interactions;
    private bool used;
    private HoldingObject objectHeld;
    // Start is called before the first frame update
    private void Reset()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        objectHeld = Player.GetComponent<HoldingObject>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") & Input.GetKey(KeyCode.E) & !used)
        { 
            Interactions.Invoke();
            used = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(GameManager.WaitThenExecute(0.1f, () => { used = false; }));
        }
    }
}
