using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Identifiers;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    //[SerializeField] private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Level1");
        //animator.SetBool("PlayerMoveOut", true);
        //StartCoroutine(PlayerOutAnimationInNewGame());
        
    }

    

    // IEnumerator PlayerOutAnimationInNewGame()
    // {
    //     yield return new WaitForSeconds(1f);
    //     SceneManager.LoadScene("Level1");
        
    // }

}
