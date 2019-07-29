using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
   public GameObject[] hazards;
   public Vector3 spawnValues;
   public int hazardCount;
   public float spawnWait;
   public float startWait;
   public float waveWait;
   private int score;
   public Text ScoreText;
   public Text RestartText;
   public Text GameOverText;
   private bool gameOver;
   private bool restart;
   private bool win;
   public Text winText;
   public Text creditText;

    void Start ()
    {
        gameOver = false;
        restart = false;
        win = false;
        RestartText.text = "";
        GameOverText.text = "";
        winText.text = "";
        creditText.text = "";
        StartCoroutine (SpawnWaves ());
        score = 0;
        UpdateScore();  
    }

    void Update ()
    {
        if (restart == true)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex ) ;
            }
        }
        if (Input.GetKey("escape"))
        {
           Application.Quit(); 
        }

    }
   IEnumerator SpawnWaves ()
   {
       yield return new WaitForSeconds (startWait);
       while(true)
       {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range (0, hazards.Length)];

                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds (waveWait);

            if (gameOver)
            {
                win = false;
                restart = true;
                RestartText.text = "Press 'ENTER' to Restart";
                break;
            }
        }
    }
    public void AddScore(int newScoreValue)
    {
        if (gameOver == false)
        {
            score += newScoreValue;
            UpdateScore();
        }
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 100)
          {
            winText.text = "You win!";
            creditText.text = "Game Created By Elizabeth Richards";
            win = true;
            gameOver = true;
            restart = true;
           }
    }


    public void GameOver()
    {
        if (win == false)
        {
            GameOverText.text = "Game Over";
            gameOver = true;
        }
        
    }
}
        

       
        
