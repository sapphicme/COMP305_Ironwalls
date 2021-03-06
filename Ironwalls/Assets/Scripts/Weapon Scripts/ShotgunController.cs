﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunController : MonoBehaviour
{
    [SerializeField] private int maxAmmo = 40;
    [SerializeField] private int ammo = 40;
    [SerializeField] private int currentClip = 8;
    [SerializeField] private int clipSize = 8;
    [SerializeField] private int pellets = 10;
    [SerializeField] private float reloadTime = 1.0f;
    [SerializeField] private float spread = 10.0f;
    [SerializeField] private float fireDelay = 1.0f;
    [SerializeField] private float range = 0.5f;
    [SerializeField] private GameObject bullet;

    private Transform bulletSpawn;
    private bool isFiring = false;
    private bool isReloading = false;
    private bool isNextRound = true;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        bulletSpawn = gameObject.GetComponent<Transform>().Find("Bullet_Spawn");
    }

    public bool AddAmmo()
    {
        if (ammo < maxAmmo)
        {
            if (ammo + clipSize <= maxAmmo)
            {
                ammo += clipSize;

            }
            else if (ammo + clipSize > maxAmmo)
            {
                ammo = maxAmmo;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

        // Update is called once per frame
        void Update()
    {      
        if (isNextRound == true && Input.GetMouseButtonDown(0))
        {
            isFiring = true;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            isReloading = true;
        }
    }

    void FixedUpdate()
    {
        if (isNextRound == false)
        {
            time += Time.deltaTime;
            if (time >= fireDelay)
            {
                time = 0;
                isNextRound = true;
            }
        }

        if (isReloading == false && currentClip != 0 && isFiring == true)
        {
            if (isNextRound == true)
            {
                for (int i = 0; i < pellets; i++)
                {
                    Destroy(Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation = gameObject.transform.parent.gameObject.transform.rotation * Quaternion.Euler(0, 0, Random.Range(-spread, spread))), range);
                }
                currentClip--;
                isNextRound = false;
                isFiring = false;
            }
        }
        else if (isReloading == false && currentClip == 0 && ammo != 0 && isFiring == true)
        {
            time += Time.deltaTime;
            if (ammo - (clipSize - currentClip) >= 0 && time >= reloadTime * clipSize)
            {
                ammo -= clipSize - currentClip;
                currentClip = clipSize;
                time = 0;
                isFiring = false;
            }
            else if (ammo - clipSize < 0 && time >= reloadTime * ammo)
            {
                currentClip += ammo;
                ammo = 0;
                time = 0;
                isFiring = false;
            }
        }
        else if (isReloading == true && currentClip == clipSize || ammo == 0)
        {
            isReloading = false;
        }
        else if (isReloading == true && currentClip < clipSize && ammo != 0)
        {
            time += Time.deltaTime;
            if (ammo - (clipSize - currentClip) >= 0 && time >= reloadTime * (clipSize - currentClip))
            {
                ammo -= clipSize - currentClip;
                currentClip = clipSize;
                time = 0;
                isReloading = false;
            }
            else if (ammo - clipSize < 0 && time >= reloadTime * ammo)
            {
                currentClip += ammo;
                ammo = 0;
                time = 0;
                isReloading = false;
            }
        }
    }
}
