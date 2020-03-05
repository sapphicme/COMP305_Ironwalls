using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerScript : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public PlayerCondition damagePlayer;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator anim;
    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("isContact", isAttacking);
            damagePlayer.maxHealth -= 2;
        }
        if(damagePlayer.maxHealth <= 0)
        {
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag != "Player")
        {
            anim.SetBool("isContact", true);
        }
    }
}
