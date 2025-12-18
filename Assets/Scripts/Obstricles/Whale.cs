using UnityEngine;

public class Whale : MonoBehaviour
{
    
    void Update()
    {
        float moveX = GameManager.Instance.worldSpeed * Time.deltaTime;
        transform.position += new Vector3(-moveX, 0);
        if(transform.position.x < -14 ) Destroy(gameObject);
    }
}
