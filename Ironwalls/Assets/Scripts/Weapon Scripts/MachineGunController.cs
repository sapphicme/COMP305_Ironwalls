using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunController : MonoBehaviour
{
    [SerializeField] private int ammo = 30;
    [SerializeField] private GameObject bullet;

    private Transform bulletSpawn;
    private float fireDelay = 0.0f;
    private bool isFiring = false;
    private float time;

    // Start is called before the first frame update
    void Start()
    {        
        bulletSpawn = gameObject.GetComponent<Transform>().Find("Bullet_Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isFiring = true;            
        } 
        else if (Input.GetMouseButtonUp(0)) 
        {
            isFiring = false;
        }
    }

    void FixedUpdate()
    {
        if (isFiring == true)
        {
            time += Time.deltaTime;
            if (time > 0.1f)
            {
                Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation = Quaternion.Euler(0, 0, Random.Range(-5.0f, 5.0f)));
                time = 0;
            }           
        }
    }
}
