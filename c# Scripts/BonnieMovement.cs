using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonnieMovement : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D Bonnie;
    private Vector2 lastFrameVec;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        Bonnie = gameObject.GetComponent<Rigidbody2D>();
        lastFrameVec = Bonnie.position;
    }
    void Update()
    {
        Vector2 temp = (Bonnie.position - lastFrameVec).normalized;

        if(Mathf.Abs(temp.x) > 0)
            animator.SetBool("Right", temp.x > 0); // True is Right, False is Left

        animator.SetBool("Moving", Mathf.Abs(temp.x) > 0);

        animator.SetFloat("VeloX", temp.x);
        animator.SetFloat("VeloY", temp.y);
        lastFrameVec = Bonnie.position;

    }
}
