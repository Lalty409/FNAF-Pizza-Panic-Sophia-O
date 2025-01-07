using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TeleportTo : MonoBehaviour
{
    public GameObject DoorOut;
    public Camera Cam;
    public Transform CameraPos;
    public Vector3 TeleportToOffset;
    public bool Used;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") & !Used)
        {
            Rigidbody2D temp = collision.gameObject.GetComponent<Rigidbody2D>();
            DoorOut.GetComponent<TeleportTo>().CoolDown();
            temp.position = DoorOut.transform.position + TeleportToOffset;
            Cam.transform.position = CameraPos.position;
        }
    }
    public void CoolDown()
    {
        this.Used = true;
        StartCoroutine(GameManager.WaitThenExecute(0.1f, () => { Used = false; })); 
    }

}
