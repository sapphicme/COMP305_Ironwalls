using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float time;
    [SerializeField] private bool isMoving;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isMoving == true)
        {
            time += Time.deltaTime / 10;
            rb.velocity /= time;
            rb.angularVelocity /= time;
        }

        if (rb.velocity != Vector2.zero || rb.angularVelocity != 0)
        {
            isMoving = true;
        }     
        else {
            isMoving = false;
            time = 1.0f;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            time = 1.0f;
            isMoving = false;
        }
    }
}
