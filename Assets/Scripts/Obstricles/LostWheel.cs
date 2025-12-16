using UnityEngine;
using UnityEngine.SceneManagement;

public class LostWheel : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        float moveX = GameManager.Instance.WorldSpeed * PlayerController.Instance.boost * Time.deltaTime;
        transform.position += new Vector3(-moveX, 0);
        if(transform.position.x < -14 ) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Level1Complete");
        }
    }
}
