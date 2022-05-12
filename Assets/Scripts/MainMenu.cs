using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_Text HighScoreText;
    private void Start()
    {
        HighScoreText.text = "Highscore : "+PlayerPrefs.GetInt(ScoreSystem.HighScoreKey).ToString();
    }
    public void LoadScene_Game()
    {
        // Loading the game screen in index 1
        SceneManager.LoadScene(1);
    }
}
