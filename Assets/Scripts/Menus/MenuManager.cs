using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Identifiers;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Level1");
    }

}
