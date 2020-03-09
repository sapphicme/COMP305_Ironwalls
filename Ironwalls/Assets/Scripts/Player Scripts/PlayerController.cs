using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int weapon = 1;
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject machineGun;
    [SerializeField] private GameObject shotgun;
    [SerializeField] private GameObject rocketLauncher;
    [SerializeField] private GameObject crate;
    [SerializeField] private int numberOfCrates = 0;
    [SerializeField] private int numberOfDetectdCrates = 0;
    [SerializeField] private bool isHolding = false;
    [SerializeField] private float throwCrate = 10.0f;

    [SerializeField] private float moveSpeed = 8.0f;
    [SerializeField] private float dash = 100.0f;
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool isCoolingDash = false;
    [SerializeField] private float dashDelayTime = 2.0f;
    [SerializeField] private bool dashW = false;
    [SerializeField] private bool dashA = false;
    [SerializeField] private bool dashS = false;
    [SerializeField] private bool dashD = false;
    
    private float time = 0.0f;
    private GameObject cratePosition;
    private Rigidbody2D rb;
    private Camera cam;
    //public Transform firePoint;
    //public GameObject bulletToFire;

    void Start()
    {
        cam = Camera.main;
        rb = gameObject.GetComponent<Rigidbody2D>();
        cratePosition = gameObject.GetComponent<Transform>().Find("Crate_Position").gameObject;
        pistol = gameObject.GetComponent<Transform>().Find("Pistol").gameObject;
        machineGun = gameObject.GetComponent<Transform>().Find("Machine_Gun").gameObject;
        shotgun = gameObject.GetComponent<Transform>().Find("Shotgun").gameObject;
        rocketLauncher = gameObject.GetComponent<Transform>().Find("Rocket_Launcher").gameObject;
    }

    void HideWeapons()
    {
        pistol.SetActive(false);
        machineGun.SetActive(false);
        shotgun.SetActive(false);
        rocketLauncher.SetActive(false);
    }

    void SwitchWeapon()
    {
        if (weapon == 1)
        {
            pistol.SetActive(true);
            machineGun.SetActive(false);
            shotgun.SetActive(false);
            rocketLauncher.SetActive(false);
        }
        else if (weapon == 2)
        {
            pistol.SetActive(false);
            machineGun.SetActive(true);
            shotgun.SetActive(false);
            rocketLauncher.SetActive(false);
        }
        else if (weapon == 3)
        {
            pistol.SetActive(false);
            machineGun.SetActive(false);
            shotgun.SetActive(true);
            rocketLauncher.SetActive(false);
        }
        else if (weapon == 4)
        {
            pistol.SetActive(false);
            machineGun.SetActive(false);
            shotgun.SetActive(false);
            rocketLauncher.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Switch Weapon
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // scroll up
        {
            if (weapon + 1 <= 4)
            {
                weapon++;
            }
            else if (weapon + 1 > 4)
            {
                weapon = 1;
            }

            SwitchWeapon();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // scroll down
        {
            if (weapon - 1 >= 1)
            {
                weapon--;
            }
            else if (weapon - 1 < 1)
            {
                weapon = 4;
            }

            SwitchWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon = 1;
            SwitchWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapon = 2;
            SwitchWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weapon = 3;
            SwitchWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            weapon = 4;
            SwitchWeapon();
        }

        // Drop Crate(s)
        if (isHolding == true && Input.GetMouseButtonUp(1))
        {
            SwitchWeapon();
            cratePosition.SetActive(false);

            for (int i = 0; i < numberOfCrates; i++)
            {
                Instantiate(crate, cratePosition.transform.position, cratePosition.transform.rotation);
            }

            isHolding = false;
            numberOfCrates = 0;
            moveSpeed = 8;
            canDash = true;
            dashDelayTime = 2;
        }

        // Throw Crate(s)
        if (isHolding == true && Input.GetMouseButtonDown(0))
        {
            throwCrate = throwCrate / numberOfCrates;
            SwitchWeapon();
            cratePosition.SetActive(false);

            for (int i = 0; i < numberOfCrates; i++)
            {
                Instantiate(crate, cratePosition.transform.position, cratePosition.transform.rotation).GetComponent<Rigidbody2D>().AddForce(transform.right * throwCrate, ForceMode2D.Impulse);
            }

            isHolding = false;
            numberOfCrates = 0;
            moveSpeed = 8;
            throwCrate = 10;
            canDash = true;
            dashDelayTime = 2;
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

        // Dash
        if (dashDelayTime != -1 && isCoolingDash == true)
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
            dashW = true;
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            dashW = false;
        }

        if (canDash == true && isCoolingDash == false && dashW == true && Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.AddForce(transform.up * dash, ForceMode2D.Impulse);
            isCoolingDash = true;
        }

        // A
        if (isCoolingDash == false && Input.GetKeyDown(KeyCode.A))
        {
            dashA = true;
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            dashA = false;
        }

        if (canDash == true && isCoolingDash == false && dashA == true && Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.AddForce(-transform.right * dash, ForceMode2D.Impulse);
            isCoolingDash = true;
        }

        // S
        if (isCoolingDash == false && Input.GetKeyDown(KeyCode.S))
        {
            dashS = true;
        }
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            dashS = false;
        }

        if (canDash == true && isCoolingDash == false && dashS == true && Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.AddForce(-transform.up * dash, ForceMode2D.Impulse);
            isCoolingDash = true;
        }

        // D
        if (isCoolingDash == false && Input.GetKeyDown(KeyCode.D))
        {
            dashD = true;
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
        {
            dashD = false;
        }

        if (canDash == true && isCoolingDash == false && dashD == true && Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.AddForce(transform.right * dash, ForceMode2D.Impulse);
            isCoolingDash = true;
        }

        if (rb.angularVelocity != 0)
        {
            rb.angularVelocity = 0;
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        canDash = false;
        if (isHolding == false && col.tag == "Crate")
        {
            numberOfCrates++;
        } 
        else if (isHolding == true)
        {
            numberOfDetectdCrates++;
        }
    }


    void OnTriggerStay2D(Collider2D col)
    {
        canDash = false;
        if (Input.GetMouseButtonDown(1) && col.tag == "Crate")
        {
            if (numberOfDetectdCrates != 0)
            {
                numberOfCrates += numberOfDetectdCrates;
                numberOfDetectdCrates = 0;
            }

            moveSpeed = moveSpeed / numberOfCrates;
            dashDelayTime = -1;

            isHolding = true;
            canDash = false;
            cratePosition.SetActive(true);
            HideWeapons();
            Destroy(col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (dashDelayTime != -1)
        {
            canDash = true;
        }

        if (isHolding == false && col.tag == "Crate")
        {
            numberOfCrates--;
        }
        else if (isHolding == true)
        {
            if (numberOfDetectdCrates != 0)
            {
                numberOfDetectdCrates--;
            }
        }
    }
}
