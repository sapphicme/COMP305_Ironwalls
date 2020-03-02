using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCondition : MonoBehaviour
{
    //Max Health of enemy
    [SerializeField] private int maxHealth = 50;
    //current health of enemy
    [SerializeField] private int currentHealth;
    //What color the sprite will blink when damaged
    [SerializeField] private Color blinkColor = Color.red;
    //How long the blink will be when damaged
    [SerializeField] private float blinkDuration = 0.5f;

    private EnemyBulletController bulletController;

    void Start()
    {
        //bulletController.GetComponent<EnemyBulletController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
