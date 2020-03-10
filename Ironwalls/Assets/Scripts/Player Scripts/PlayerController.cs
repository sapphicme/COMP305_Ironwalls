using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    [SerializeField] private int numberOfCrates;
    [SerializeField] private bool isHolding;

    [SerializeField] private float moveSpeed = 8.0f;
    [SerializeField] private GameObject dashTrail;
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

    void Start()
    {
        cam = Camera.main;
        rb = gameObject.GetComponent<Rigidbody2D>();
        dashTrail = gameObject.GetComponent<Transform>().Find("Player_Top").gameObject.GetComponent<Transform>().Find("Dash_Trail").gameObject;
    }  

    void DisableDashTrail()
    {
        dashTrail.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        isHolding = gameObject.GetComponent<Transform>().Find("Player_Top").gameObject.GetComponent<PlayerController2>().IsHolding();
        numberOfCrates = gameObject.GetComponent<Transform>().Find("Player_Top").gameObject.GetComponent<PlayerController2>().NumberOfCrates();

        if (isHolding == true)
        {
            moveSpeed = 8 / numberOfCrates;
        } else
        {
            moveSpeed = 8;
        }   

        // Drop Crate(s)
        if (isHolding == true && Input.GetMouseButtonUp(1))
        {            
            canDash = true;
            dashDelayTime = 2;
        }

        //    // Throw Crate(s)
        if (isHolding == true && Input.GetMouseButtonDown(0))
        {           
            canDash = true;
            dashDelayTime = 2;
        }

        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;       

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
            dashTrail.SetActive(true);
            rb.AddForce(transform.up * dash, ForceMode2D.Impulse);
            Invoke("DisableDashTrail", 0.3f);
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
            dashTrail.SetActive(true);
            rb.AddForce(-transform.right * dash, ForceMode2D.Impulse);
            Invoke("DisableDashTrail", 0.3f);
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
            dashTrail.SetActive(true);
            rb.AddForce(-transform.up * dash, ForceMode2D.Impulse);
            Invoke("DisableDashTrail", 0.3f);
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
            dashTrail.SetActive(true);
            rb.AddForce(transform.right * dash, ForceMode2D.Impulse);
            Invoke("DisableDashTrail", 0.3f);
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
    }


    void OnTriggerStay2D(Collider2D col)
    {
        canDash = false;        
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (dashDelayTime != -1)
        {
            canDash = true;
        }        
    }
}
