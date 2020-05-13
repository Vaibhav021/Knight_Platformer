using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    private string sceneName;

    public static bool isPaused;
    public bool isDead; //connected to 
    public static bool isWin;  //player death script

    public GameObject cursorSword;
    public GameObject pauseMenuUI;
    public GameObject LevelWinMenuUI;
    public GameObject WinMenuUI;
    public GameObject FailMenuUI;


    void Start()
    {
        isPaused = false;
        isDead = false;
        isWin = false;            

        pauseMenuUI.SetActive(false);
        LevelWinMenuUI.SetActive(false);
        WinMenuUI.SetActive(false);
        FailMenuUI.SetActive(false);

        Time.timeScale = 1f;

        //Check Current Scene
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        //For Cursor        

        if (sceneName == "MainMenu")
            cursorSword.SetActive(true);
        else 
            cursorSword.SetActive(false);

    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName != "MainMenu")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused == true)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        if(isDead == true)
        {
            AfterDeath();
        }

        if(isWin == true)
        {
            Invoke("AfterWin", 5f);
        }


    }

    #region Level Menu System

    public void Pause()
    {
        cursorSword.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);

    }    

    public void Resume()
    {
        cursorSword.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void AfterDeath()
    {
        cursorSword.SetActive(true);
        Time.timeScale = 0f;
        FailMenuUI.SetActive(true);
    }

    public void AfterWin()
    {
        cursorSword.SetActive(true);
        //Time.timeScale = 0f;

        //Scene currentLevel = SceneManager.GetActiveScene();

        //if (currentLevel.name == "Level_3")
            WinMenuUI.SetActive(true);
        //else
        //    LevelWinMenuUI.SetActive(true);

    }

    #endregion


    #region Buttons Functions



    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level_2");
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level_3");
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene("Level_BD");
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Scene currentLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentLevel.name);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }




    #endregion


}//class
