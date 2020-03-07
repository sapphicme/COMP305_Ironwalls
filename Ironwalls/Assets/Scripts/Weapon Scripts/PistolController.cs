using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolController : MonoBehaviour
{
    [SerializeField] private int currentClip = 7;
    [SerializeField] private int clipSize = 7;
    [SerializeField] private float reloadTime = 2.0f;
    [SerializeField] private float spread = 5.0f;
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

        if (currentClip != 0 && isFiring == true)
        {
            if (isNextRound == true)
            {                
                Destroy(Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation = gameObject.transform.parent.gameObject.transform.rotation * Quaternion.Euler(0, 0, Random.Range(-spread, spread))), range);
                
                currentClip--;
                isNextRound = false;
                isFiring = false;
            }
        }
        else if (currentClip == 0 && isFiring == true)
        {
            time += Time.deltaTime;
            if (time >= reloadTime)
            {                
                currentClip = clipSize;
                time = 0;
            }            
        }
        else if (isReloading == true && currentClip < clipSize)
        {
            time += Time.deltaTime;
            if (time >= reloadTime)
            {                
                currentClip = clipSize;
                time = 0;
            }            
        }
    }
}
