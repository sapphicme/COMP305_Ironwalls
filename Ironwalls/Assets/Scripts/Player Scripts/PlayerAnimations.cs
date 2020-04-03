using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator anim;
    private bool isMoving = false;
    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

            if (rb.velocity.x > 0.1 || rb.velocity.y > 0.1)
            {
                isMoving = true;
            }
        else
        {
            isMoving = false;
        }
            anim.SetBool("isMoving", isMoving);

        
    }
}
