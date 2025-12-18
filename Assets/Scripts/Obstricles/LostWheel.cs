using UnityEngine;
using UnityEngine.SceneManagement;

public class LostWheel : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Level1Complete");
        }
    }
}
