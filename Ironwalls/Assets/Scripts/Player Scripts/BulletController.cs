using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float speed = 10f;
    public Rigidbody2D body;
    public BossScript boss;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if(other.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if (other.tag == "Boss")
        {
            boss.health -= 5;
            Destroy(gameObject);
        }
        if (other.tag == "Boss" && boss.health <= 0)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
