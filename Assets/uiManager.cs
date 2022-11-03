using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
    public Text scoreText;
    bool gameOver;
    private int score;
    public Button[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        gameOver = false;
        InvokeRepeating("scoreUpdate", 1.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
        //score += 1;
    }

    void scoreUpdate()
    {
        if(!gameOver)
        {
            score += 1;
        }
    }

    public void gameOverActivate()
    {
        gameOver = true;
        foreach(Button button in buttons)
        {
            button.gameObject.SetActive(true);
        }
    }

    public void Play(string Level1)
    {
        SceneManager.LoadScene(Level1);
    }

    public void Pause()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    /*
    public void Retry(string Level1)
    {
        //SceneManager.LoadScene(Level1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    */

    public void mainMenu(string Menu)
    {
        SceneManager.LoadScene(Menu);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
