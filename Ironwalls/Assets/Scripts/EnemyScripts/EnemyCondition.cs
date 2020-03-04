using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/EnemyStats")]
public class EnemyCondition : ScriptableObject
{
    //Max Health of enemy
    [Header("Health State")]
    public int maxHealth = 50;
    //current health of enemy
    public int currentHealth;
    //What color the sprite will blink when damaged
    public Color blinkColor = Color.red;
    //How long the blink will be when damaged
    public float blinkDuration = 0.5f;
}
