using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float worldSpeed;
    public float critterCount;
    private ObjectPooler objectPooler;


    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }
    void Start()
    {
        objectPooler = GameObject.Find("Boss1Pool").GetComponent<ObjectPooler>();
        critterCount = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Fire3"))
        {
            Pause();
        }

        if (critterCount >= 5)
        {
            critterCount = 0;
            GameObject destroyEffect = objectPooler.GetPoolObject();
            destroyEffect.transform.position = new Vector3(13f, Random.Range(-3f, 3f));
            destroyEffect.transform.rotation = Quaternion.Euler(0, 0, -90);
            destroyEffect.SetActive(true);
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
