using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    /* TODO: 
    Write code to take damage
    Add Slider/UI component - double check where health bar should be displayed
    Add animation to on hit, death
    Add audioclips - on hit and on death
    Add repawn to death method - find out how respawn mechanic works
    Add disable movement + shooting to playercontroller + bullet controller.
    */

    //"Public" Variables
    //Max Health of player
    [SerializeField]private int maxHealth = 100;
    //current health of player
    [SerializeField]private int currentHealth;
    //Number of lives 
    [SerializeField]private int lives = 3;
    //What color the sprite will blink when damaged
    [SerializeField]private Color blinkColor = Color.red;
    //How long the blink will be when damaged
    [SerializeField]private float blinkDuration = 0.5f;

    //Private Variables
    //Check for when player is damaged
    private bool isDamaged = false;
    //Check for when player is dead
    private bool isDead = false;
    //Records color for damage blink
    private Color defaultColor;
    

    //initializing components 
    private SpriteRenderer sprite;
    private Animator animator;
    private PlayerController playerController;
    private BulletController bulletController;


    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        //set up references
        animator.GetComponent<Animator>();
        playerController.GetComponent<PlayerController>();
        bulletController.GetComponent<BulletController>();

        currentHealth = maxHealth;
        defaultColor = sprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDamaged)
        {
            StartCoroutine("animateDamage");
        }
    }
    //Take Damage - can change depending on if everything hits for the same amount
    //Will kill the player if their health hits 0
    public void takeDamage(int amount)
    {
        isDamaged = true;

        currentHealth -= amount;

        if(currentHealth <= 0 && !isDead)
        {
            death();
        }

        isDamaged = false;
    }

    //Kills the player - needs to disable movement/shooting capabilities
    //Will need to be modified if we get to coop assuming you can pick each other up
    void death()
    {
        isDead = true;
        
        lives -= 1;

        animator.SetTrigger("Die");

    }
    IEnumerable animateDamage()
    {
        sprite.color = blinkColor;
        yield return new WaitForSeconds(blinkDuration);
        sprite.color = defaultColor;
    }

}
