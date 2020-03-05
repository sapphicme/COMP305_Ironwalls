using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject machineGun;
    [SerializeField] private GameObject shotgun;
    [SerializeField] private GameObject rocketLauncher;

    private int weapon = 1; 
    private Rigidbody2D rb;
    private float moveSpeed = 8;
    private Camera cam;
    //public Transform firePoint;
    //public GameObject bulletToFire;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        machineGun = gameObject.GetComponent<Transform>().Find("Machine_Gun").gameObject;
        shotgun = gameObject.GetComponent<Transform>().Find("Shotgun").gameObject;
        rocketLauncher = gameObject.GetComponent<Transform>().Find("Rocket_Launcher").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // scroll up
        {
            if (weapon + 1 <= 3)
            {
                weapon++;
            } 
            else if (weapon + 1 > 3)
            {
                weapon = 1;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // scroll down
        {
            if (weapon - 1 >= 1)
            {
                weapon--;
            }
            else if (weapon - 1 < 1)
            {
                weapon = 3;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) || weapon == 1)
        {
            machineGun.SetActive(true);
            shotgun.SetActive(false);
            rocketLauncher.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || weapon == 2)
        {
            machineGun.SetActive(false);
            shotgun.SetActive(true);
            rocketLauncher.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || weapon == 3)
        {
            machineGun.SetActive(false);
            shotgun.SetActive(false);
            rocketLauncher.SetActive(true);
        }

        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
        Vector3 mouse = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Instantiate(bulletToFire, firePoint.position, transform.rotation);
        //}
    }
}
