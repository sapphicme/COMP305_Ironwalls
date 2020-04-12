using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private Animator anim;
    public float speed = 10f;
    public Rigidbody2D body;
    public PlayerCondition damagePlayer;

    // Start is called before the first frame update
    public void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        damagePlayer = GameObject.FindWithTag("Player").GetComponent<PlayerCondition>();
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            damagePlayer.maxHealth -= 15;
            Destroy(gameObject);
            anim.SetBool("isContact", anim);
        }
        if (damagePlayer.maxHealth <= 0)
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "EnemyBullet")
        {
            Destroy(gameObject);
        }
    }
}
