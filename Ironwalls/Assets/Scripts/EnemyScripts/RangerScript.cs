using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private float currentShot;
    private bool shot;
    private Animator anim;

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public Transform player;

    public float delayShot;
    public GameObject bullet;
    public Transform bulletSpawn;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            anim.SetBool("isShooting", false);
        }
        else if(Vector2.Distance(transform.position, player.position)> stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if(Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            anim.SetBool("isShooting", false);
        }

        if(currentShot == 0)
        {
            Shoot();
        }
        if (shot && currentShot < delayShot)
        {
            currentShot += 1 * Time.deltaTime;
        }
        if(currentShot >= delayShot)
        {
            currentShot = 0;
        }
        anim.SetBool("isShooting", anim);
    }

    public void Shoot()
    {
        shot = true;
        GameObject clone = Instantiate(bullet, bulletSpawn.position, transform.rotation) as GameObject;
        clone.AddComponent<PlayerCondition>();
        Debug.Log("Shot");
    }
}

