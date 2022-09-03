using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int level = 1;
    private int score = 0;
    private int maxLifes = 3;
    private int lifes;
    private int timeBetweenLevels = 15; // in seconds
    private SpawnManager spawnManager;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI lifesText;


    private void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        lifes = maxLifes;
        StartCoroutine(NextLevel());
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(timeBetweenLevels);
        level += 1;
        levelText.text = "Level: " + level.ToString();
        spawnManager.IncreaseSpawnChances();
        if(level % 5 == 0)
        {
            if(lifes < maxLifes)
            {
                lifes++;
                lifesText.text = "Lifes: " + lifes.ToString();
            }

            spawnManager.DecreaseSpawnInterval();

            if(level % 10 == 0)
            {
                spawnManager.IncreaseAnimalsSpeed();
                lifes = maxLifes;
                lifesText.text = "Lifes: " + lifes.ToString();
            }
        }
        StartCoroutine(NextLevel());
    }

    public void addScore(int newScore)
    {
        score += newScore * level;
        scoreText.text = "Score: " + score.ToString();
    }

    public void damageTaken()
    {
        lifes--;
        lifesText.text = "Lifes: " + lifes.ToString();
        if (lifes <= 0)
        {
            gameOver();
        }
    }

    void gameOver()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Level", level);
        SceneManager.LoadScene("GameOver");
    }
}
