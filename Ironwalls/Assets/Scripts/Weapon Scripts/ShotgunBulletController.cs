using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBulletController : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    private Rigidbody2D rbody;
    public BossScript boss;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        boss = GameObject.FindWithTag("Boss").GetComponent<BossScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        rbody.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if (other.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if (other.tag == "Boss")
        {
            boss.health -= 2;
            Destroy(gameObject);
        }
        if (other.tag == "Boss" && boss.health <= 0)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
