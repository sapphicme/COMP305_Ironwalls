﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrollScript : MonoBehaviour
{
    // public variables
    public float speed;

    //gameobjects 
    public GameObject stopCol;
    public GameObject slowCol;
    public GameObject slowColTwo;
    public GameObject trainCol;
    public GameObject platformPlayer;
    public GameObject trainPlayer;
    public GameObject mainCamera;

    //audio sources 
    public AudioSource audioSource;
    public AudioClip trainHorn;

    public AudioSource audioTwo;
    public AudioClip trainTrack;
    


    // Start is called before the first frame update
    void Start()
    {
        platformPlayer.gameObject.SetActive(false);
        trainPlayer.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // to move the train up 
        transform.Translate(Vector3.up * Time.deltaTime * speed);

        // to play train horn

        if (Input.GetKey(KeyCode.H))
        {
            //play sound while object is rotated
            audioSource.clip = trainHorn;
            audioSource.Play();
            Debug.Log("Play sound");
        }

        if(speed == 0)
        {
            platformPlayer.gameObject.SetActive(true);
            trainPlayer.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
            audioSource.volume = 0.0f;
        }


    }

    // to detect collision with trigger to slow down the train before the station
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == slowCol)
        {
            // to begin slowing the train down before it stops
            speed = 6;
            Debug.Log("I am slowing down!");
            audioTwo.volume = 0.25f;
            Debug.Log("volume fade");
        }

        if (other.gameObject == slowColTwo)
        {
            // to begin slowing the train down before it stops
            speed = 4;
            Debug.Log("I am slowing down!");
            audioTwo.volume = 0.10f;
            Debug.Log("volume fade");
        }

        if (other.gameObject == stopCol)
        {
            //to stop the trains movement
            speed = 0;
            Debug.Log("I have stopped");
            audioTwo.volume = 0.0f;
            Debug.Log("volume off");
            trainCol.SetActive(false);
            

            //SceneManager.LoadScene("Player_LevelOne");
        }
    }

}
