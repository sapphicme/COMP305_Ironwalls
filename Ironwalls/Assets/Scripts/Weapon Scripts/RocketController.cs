using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    private Rigidbody2D rbody;
    private GameObject rocketImage;
    private GameObject rocketTrail;
    private GameObject explosion;
    private bool isStop = false;
    public BossScript boss;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rocketImage = gameObject.GetComponent<Transform>().Find("Rocket_Image").gameObject;
        rocketTrail = gameObject.GetComponent<Transform>().Find("Rocket_Trail").gameObject;
        explosion = gameObject.GetComponent<Transform>().Find("Explosion").gameObject;
        boss = GameObject.FindWithTag("Boss").GetComponent<BossScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStop == true)
        {
            rocketImage.SetActive(false);
            rocketTrail.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (isStop == false)
        {
            rbody.velocity = transform.right * speed;
        }
        else if (isStop == true)
        {
            rbody.velocity = transform.right * speed * 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            isStop = true;
            Destroy(gameObject, 0.1f);
            explosion.SetActive(true);
        }
        if (other.tag == "Wall")
        {
            isStop = true;
            Destroy(gameObject, 1.0f);
            explosion.SetActive(true);
        }
        if (other.tag == "Boss")
        {
            boss.health -= 50;
            Destroy(gameObject);
        }
        if (other.tag == "Boss" && boss.health <= 0)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
