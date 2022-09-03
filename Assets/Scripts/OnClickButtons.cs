using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class OnClickButtons : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;


    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            scoreText.text = "Top Score: " + PlayerPrefs.GetInt("HighScore").ToString();
            levelText.text = "Top Level: " + PlayerPrefs.GetInt("HighLevel").ToString();
        }
        else if(SceneManager.GetActiveScene().name == "GameOver")
        {
            int score = PlayerPrefs.GetInt("Score");
            int level = PlayerPrefs.GetInt("Level");
            scoreText.text = "Score: " + score.ToString();
            levelText.text = "Level: " + level.ToString();
            if(score > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", score);
                scoreText.text = "New Top " + scoreText.text;
            }
            if (level > PlayerPrefs.GetInt("HighLevel"))
            {
                PlayerPrefs.SetInt("HighLevel", level);
                levelText.text = "New Top " + levelText.text;
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Prototype 2");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
