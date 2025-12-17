using System.Collections;
using UnityEngine;

public class Astroid : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    
    private Material defaultMaterial;
    [SerializeField]private Material whiteMaterial;



    [SerializeField] private Sprite[] sprites;




    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        defaultMaterial = spriteRenderer.material;
        
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spriteRenderer.material = whiteMaterial;
            StartCoroutine(ResetMaterial());
        }
    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(.2f);
        spriteRenderer.material = defaultMaterial;
        
    }


}
