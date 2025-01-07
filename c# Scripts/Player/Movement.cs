using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator controller;
    private Animator Foodcontroller;
    private Vector2 lastPos;
    public bool InDialogue;
    public bool facing;
    private bool moving;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<Animator>();
        Foodcontroller = gameObject.transform.Find("ThingHeld").GetComponent<Animator>();
    }
    // freddy go brrrr
    // Update is called once per frame
    void Update()
    {
        if (InDialogue)
        {
            controller.SetFloat("VelocityMagnitude", 0f);
            return;
        }
        Vector2 movementVec = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.A))
        {
            movementVec.x -= 1;
            facing = false;
            // left facing = false
            moving = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementVec.x += 1;
            // right facing = true
            facing = true;
            moving = true;
        }
        if (Input.GetKey(KeyCode.S))
        { 
            movementVec.y -= 1;
            moving = true;
        }
        if (Input.GetKey(KeyCode.W))
        {
            movementVec.y += 1;
            moving = true;
        }
        if (!moving)
        {
            rb.velocity = new Vector3(0, 0, 0);   
        }
        controller.SetBool("Facing", facing);
        Foodcontroller.SetBool("Facing", facing);

        controller.SetFloat("VelocityMagnitude", Mathf.Clamp(rb.velocity.magnitude,0.005f,1f));

        movementVec.Normalize();
        rb.AddForce(movementVec * speed);

        
        lastPos = rb.position;
    }

    public void setDialogueVal(bool DialogueVal)
    {
        this.InDialogue = DialogueVal;
    }
    public bool getDialogueVal()
    {
        return this.InDialogue;
    }
}
