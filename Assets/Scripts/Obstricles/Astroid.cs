using System.Collections;
using UnityEngine;

public class Astroid : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private Material defaultMaterial;
    [SerializeField] private Material whiteMaterial;
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private float life;



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

        float randomScale = Random.Range(0.6f, 1f);
        transform.localScale = new Vector3(randomScale, randomScale);

    }

    void Update()
    {
        float moveX = GameManager.Instance.worldSpeed * Time.deltaTime;
        transform.position += new Vector3(-moveX, 0);
        if (transform.position.x < -11) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet")) TakeDamage(1);
        else if (collision.gameObject.CompareTag("Boss")) TakeDamage(5);

    }

    public void TakeDamage(int damage)
    {
        spriteRenderer.material = whiteMaterial;
        StartCoroutine(ResetMaterial());
        AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.hitRock);

        life -= damage;
        if (life <= 0)
        {
            Instantiate(destroyEffect, transform.position, transform.rotation);
            AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.boom2);
            Destroy(gameObject);
        }

    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(.2f);
        spriteRenderer.material = defaultMaterial;

    }


}
