using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int addedHealth = 25;
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

    private GameObject cratePosition;
    

    void Start()
    {        
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

    public bool IsHolding()
    {
        return this.isHolding;
    }

    public int NumberOfCrates()
    {
        return this.numberOfCrates;
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
            throwCrate = 10;            
        }

        Vector3 mouse = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (isHolding == false && col.tag == "Crate")
        {
            numberOfCrates++;
        }
        else if (isHolding == true)
        {
            numberOfDetectdCrates++;
        }

        if (col.tag == "Health Pack")
        {
            if (currentHealth < maxHealth)
            {
                if (currentHealth + addedHealth <= maxHealth)
                {
                    currentHealth += addedHealth;
                }
                else if (currentHealth + addedHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
                Destroy(col.gameObject);
            }
        }
    }


    void OnTriggerStay2D(Collider2D col)
    {
        if (Input.GetMouseButtonDown(1) && col.tag == "Crate")
        {
            if (numberOfDetectdCrates != 0)
            {
                numberOfCrates += numberOfDetectdCrates;
                numberOfDetectdCrates = 0;
            }            

            isHolding = true;
            cratePosition.SetActive(true);
            HideWeapons();
            Destroy(col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {       
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
