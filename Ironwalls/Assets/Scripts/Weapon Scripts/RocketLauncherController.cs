using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherController : MonoBehaviour
{
    [SerializeField] private int ammo = 5;
    [SerializeField] private int currentClip = 1;
    [SerializeField] private int clipSize = 1;
    [SerializeField] private float reloadTime = 3.0f;
    [SerializeField] private GameObject rocket;

    private Transform rocketSpawn;
    private bool isFiring = false;
    private bool isReloading = false;
    private bool isNextRound = true;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        rocketSpawn = gameObject.GetComponent<Transform>().Find("Rocket_Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        if (isNextRound = true && Input.GetMouseButtonDown(0))
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
            if (time >= reloadTime)
            {
                time = 0;
                isNextRound = true;
            }
        }

        if (isReloading == false && currentClip != 0 && isFiring == true)
        {
            if (isNextRound == true)
            {
                Instantiate(rocket, rocketSpawn.position, rocketSpawn.rotation = gameObject.transform.parent.gameObject.transform.rotation);
                currentClip--;
                isNextRound = false;
                isFiring = false;
            }
        }
        else if (isReloading == false && currentClip == 0 && ammo != 0 && isFiring == true)
        {
            time += Time.deltaTime;
            if (ammo - (clipSize - currentClip) >= 0 && time >= reloadTime * (clipSize - currentClip))
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
