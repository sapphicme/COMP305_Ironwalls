using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrateControllerLevelTwo : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float time;
    [SerializeField] private bool isMoving;

    public int pointsToAdd;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void IsCollected()
    {
        Debug.Log("Crate Collected");
        ScoreManager.AddPoints(pointsToAdd);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    void FixedUpdate()
    {
        if (isMoving == true)
        {
            time += Time.deltaTime / 10;
            rb.velocity /= time;
            rb.angularVelocity /= time;
        }

        if (rb.velocity != Vector2.zero || rb.angularVelocity != 0)
        {
            isMoving = true;
        }     
        else {
            isMoving = false;
            time = 1.0f;
        }
        

        //level two 
        if (ScoreManager.initialScore == 4)
        {
            SceneManager.LoadScene("TransitionTwo");
            
        }
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            time = 1.0f;
            isMoving = false;
        }
    }
}
