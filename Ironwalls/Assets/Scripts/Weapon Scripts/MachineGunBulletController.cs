using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunBulletController : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    private Rigidbody2D rbody;
    public BossScript boss;
    public RangerScript ranger;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        boss = GameObject.FindWithTag("Boss").GetComponent<BossScript>();
        ranger = GameObject.FindWithTag("Enemy").GetComponent<RangerScript>();
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
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Boss")
        {
            boss.health -= 10;
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Boss" && boss.health <= 0)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
