using System.Collections;
using UnityEngine;

public class Astroid : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private FlashWhite flashWhite;
    [SerializeField] private GameObject destroyEffect;
    private int health;
    private int damage;
    [SerializeField] private Sprite[] sprites;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        flashWhite = GetComponent<FlashWhite>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        float moveX = Random.Range(-1f, 0f);
        float moveY = Random.Range(-1f, 1f);
        rb.linearVelocity = new Vector2(moveX, moveY);
        float randomScale = Random.Range(0.6f, 1f);
        transform.localScale = new Vector3(randomScale, randomScale);



        health = 10;
        damage = 2;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Boss"))
        {
            Boss1 boss1 = collision.gameObject.GetComponent<Boss1>();
            if (boss1) boss1.TakeDamage(damage);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController) playerController.TakeDamage(damage);

        }

    }

    public void TakeDamage(int damage)
    {
        flashWhite.Flash();
        AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.hitRock);
        health -= damage;
        if (health <= 0)
        {
            Instantiate(destroyEffect, transform.position, transform.rotation);
            AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.boom2);
            Destroy(gameObject);
        }

    }


}
