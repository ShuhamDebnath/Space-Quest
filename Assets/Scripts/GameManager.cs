using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;


    public float WorldSpeed;

    void Awake()
    {
        if(Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Fire3"))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if(UIController.Instance.pausePannel.activeSelf == false)
        {
            UIController.Instance.pausePannel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            UIController.Instance.pausePannel.SetActive(false);
            Time.timeScale = 1f;
        }

        PlayerController.Instance.ExitBoost();
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    

    public void MoveToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GameOver()
    {
        StartCoroutine(GameOverScreen()); 
    }

    IEnumerator GameOverScreen()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameOver");
    }






}
