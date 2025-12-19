using System;
using UnityEngine;

public class FloatInSpace : MonoBehaviour
{

    void Update()
    {
        float moveX = GameManager.Instance.worldSpeed * Time.deltaTime;
        transform.position += new Vector3(-moveX, 0);
        if (Mathf.Abs(transform.position.x) > 16 || Math.Abs(transform.position.y) > 8 ) gameObject.SetActive(false);
    }
}
