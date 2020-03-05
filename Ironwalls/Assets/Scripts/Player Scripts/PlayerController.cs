using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8;
    [SerializeField] private float dash = 300;
    [SerializeField] private float dashDelayTime = 2.0f;
    [SerializeField] private int weapon = 1;
    [SerializeField] private GameObject machineGun;
    [SerializeField] private GameObject shotgun;
    [SerializeField] private GameObject rocketLauncher;

    private int doubleTapW = 0;
    private int doubleTapA = 0;
    private int doubleTapS = 0;
    private int doubleTapD = 0;
    private bool isCoolingDash = false;
    private float time = 0.0f;         
    private Rigidbody2D rb;
    private Camera cam;
    //public Transform firePoint;
    //public GameObject bulletToFire;

    void Start()
    {
        cam = Camera.main;
        rb = gameObject.GetComponent<Rigidbody2D>();
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

        if (isCoolingDash == true)
        {
            time += Time.deltaTime;

            if (time >= dashDelayTime)
            {
                time = 0;
                isCoolingDash = false;
            }
        }

        // W
        if (isCoolingDash == false && Input.GetKeyDown(KeyCode.W))
        {
            doubleTapW++;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            doubleTapW = 0;
        }

        if (isCoolingDash == false && doubleTapW == 2)
        {
            rb.AddForce(transform.up * dash, ForceMode2D.Impulse);
            doubleTapW = 0;
            isCoolingDash = true;
        }

        // A
        if (isCoolingDash == false && Input.GetKeyDown(KeyCode.A))
        {
            doubleTapA++;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            doubleTapA = 0;
        }

        if (isCoolingDash == false && doubleTapA == 2)
        {
            rb.AddForce(-transform.right * dash, ForceMode2D.Impulse);
            doubleTapA = 0;
            isCoolingDash = true;
        }

        // S
        if (isCoolingDash == false && Input.GetKeyDown(KeyCode.S))
        {
            doubleTapS++;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            doubleTapS = 0;
        }

        if (isCoolingDash == false && doubleTapS == 2)
        {
            rb.AddForce(-transform.up * dash, ForceMode2D.Impulse);
            doubleTapS = 0;
            isCoolingDash = true;
        }

        // D
        if (isCoolingDash == false && Input.GetKeyDown(KeyCode.D))
        {
            doubleTapD++;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
        {
            doubleTapD = 0;
        }

        if (isCoolingDash == false && doubleTapD == 2)
        {
            rb.AddForce(transform.right * dash, ForceMode2D.Impulse);
            doubleTapD = 0;
            isCoolingDash = true;
        }
    }
}
