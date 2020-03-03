using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunController : MonoBehaviour
{
    [SerializeField] private int ammo = 120;
    [SerializeField] private int currentClip = 30;
    [SerializeField] private int clipSize = 30;
    [SerializeField] private float reloadTime = 2.0f;
    [SerializeField] private float spread = 5.0f;
    [SerializeField] private float fireDelay = 0.1f;
    [SerializeField] private float range = 1.0f;
    [SerializeField] private GameObject bullet;

    private Transform bulletSpawn;
    private bool isFiring = false;
    private bool isReloading = false;
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

        if (Input.GetKeyUp(KeyCode.R))
        {
            isReloading = true;
        }
    }

    void FixedUpdate()
    {
        if (currentClip != 0 && isFiring == true)
        {
            time += Time.deltaTime;
            if (time >= fireDelay)
            {
                Destroy(Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation = Quaternion.Euler(0, 0, Random.Range(-spread, spread))), range);
                currentClip--;
                time = 0;
            }
        }
        else if (currentClip == 0 && ammo != 0 && isFiring == true)
        {
            time += Time.deltaTime;
            if (ammo - clipSize >= 0 && time >= reloadTime)
            {
                ammo -= clipSize;
                currentClip = 30;
                time = 0;
            }
            else if (ammo - clipSize < 0 && time >= reloadTime)
            {
                currentClip = ammo;
                ammo = 0;
                time = 0;
            }            
        }
        else if (isReloading == true && currentClip < clipSize && ammo != 0)
        {
            time += Time.deltaTime;
            if (ammo - clipSize >= 0 && time >= reloadTime)
            {
                ammo -= clipSize;
                currentClip = 30;
                time = 0;
            }
            else if (ammo - clipSize < 0 && time >= reloadTime)
            {
                currentClip = ammo;
                ammo = 0;
                time = 0;
            }            
        }
    }
}
