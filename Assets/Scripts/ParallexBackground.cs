using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ParallexBackground : MonoBehaviour
{


    [SerializeField] private float moveSpeed;
    float backgrundImageWidth;

    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        backgrundImageWidth = sprite.texture.width / sprite.pixelsPerUnit;
        //Debug.Log(backgrundImageWidth);

    }

    void Update()
    {
        float moveX = moveSpeed * GameManager.Instance.worldSpeed * Time.deltaTime;
        transform.position = transform.position + new Vector3(moveX, 0);

        if (Math.Abs(transform.position.x) - backgrundImageWidth > 0)
        {
            transform.position = new Vector3(0, 0);
        }

    }
}
