using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float worldSpeed;
    public float critterCount;
    [SerializeField] private GameObject boss1;


    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Fire3"))
        {
            Pause();
        }

        if (critterCount == 20)
        {
            critterCount = 0;
            Instantiate(boss1, new Vector3(15f, 0, 0), Quaternion.Euler(0, 0, -90));
        }
    }

    public void Pause()
    {
        if (UIController.Instance.pausePannel.activeSelf == false)
        {
            UIController.Instance.pausePannel.SetActive(true);
            Time.timeScale = 0f;
            AudioManager.Instance.PlaySound(AudioManager.Instance.pause);
        }
        else
        {
            UIController.Instance.pausePannel.SetActive(false);
            Time.timeScale = 1f;
            AudioManager.Instance.PlaySound(AudioManager.Instance.unPause);
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

    public void SetWorldSpeed(float speed)
    {
        worldSpeed = speed;
    }






}
