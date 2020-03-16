using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int initialScore = 0;

    [SerializeField]
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        initialScore = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (initialScore < 0)
            initialScore = 0;
            scoreText.text = "" + initialScore;
        
    }

    public static void AddPoints(int pointsToAdd)
    {
        initialScore += pointsToAdd;
    }

    public static void Reset()
    {
        initialScore = 0;
    }
    

}
