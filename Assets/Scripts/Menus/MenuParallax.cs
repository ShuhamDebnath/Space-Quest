using System;
using UnityEngine;

public class MenuParallax : MonoBehaviour
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
        float moveX = moveSpeed  * Time.deltaTime;
        transform.position = transform.position + new Vector3(moveX, 0);

        if(Math.Abs(transform.position.x) - backgrundImageWidth > 0)
        {
            transform.position = new Vector3(0,0);
        }
        
    }
}
