using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateCollectionController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerController2 playerController2;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController2 = player.GetComponent<Transform>().Find("Player_Top").gameObject.GetComponent<PlayerController2>();
    }
    

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Crate")
        {
            Destroy(col.gameObject, 0.8f);
            col.gameObject.GetComponent<CrateController>().IsCollected();
            playerController2.AddToCollection();
            
        }
    }

    
}
