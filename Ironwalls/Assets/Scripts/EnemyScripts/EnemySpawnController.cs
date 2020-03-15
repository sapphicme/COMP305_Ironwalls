using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float currentSpawn;
    private bool spawn;

    public Transform player;
    public float delaySpawn;
    public GameObject enemy;
    public Transform enemySpawn;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpawn == 0)
        {
            Spawn();
        }
        if (spawn && currentSpawn < delaySpawn)
        {
            currentSpawn += 1 * Time.deltaTime;
        }
        if (currentSpawn >= delaySpawn)
        {
            currentSpawn = 0;
        }
    }

    public void Spawn()
    {
        spawn = true;
        GameObject clone = Instantiate(enemy, enemySpawn.position, transform.rotation) as GameObject;
        clone.AddComponent<PlayerCondition>();
        clone.AddComponent<RunnerScript>();
        Debug.Log("Spawn");
    }
}
