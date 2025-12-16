using UnityEngine;

public class Astroid : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;



    [SerializeField] private Sprite[] sprites;




    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        float moveX = Random.Range(-1f, 0f);
        float moveY = Random.Range(-1f, 1f);
        rb.linearVelocity = new Vector2(moveX, moveY);
        
    }

    void Update()
    {

        float moveX = GameManager.Instance.WorldSpeed * PlayerController.Instance.boost * Time.deltaTime;
        transform.position += new Vector3(-moveX, 0);
        if(transform.position.x < -11 ) Destroy(gameObject);
        
    }
}
