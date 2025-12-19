using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Astroid : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private FlashWhite flashWhite;
    private ObjectPooler objectPooler;
    private int health;
    private int maxHealth = 5;
    private int damage = 2;
    private int experianceToGive = 1;
    [SerializeField] private Sprite[] sprites;

    float moveX;
    float moveY;

    void OnEnable()
    {
        health = maxHealth;
        transform.rotation = Quaternion.identity;
        
        moveX = Random.Range(-1f, 0f);
        moveY = Random.Range(-1f, 1f);
        if(rb) rb.linearVelocity = new Vector2(moveX, moveY);
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        flashWhite = GetComponent<FlashWhite>();
        objectPooler = GameObject.Find("Boom2Pool").GetComponent<ObjectPooler>();


        moveX = Random.Range(-1f, 0f);
        moveY = Random.Range(-1f, 1f);
        rb.linearVelocity = new Vector2(moveX, moveY);
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        float randomScale = Random.Range(0.6f, 1f);
        transform.localScale = new Vector3(randomScale, randomScale);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Boss"))
        {
            Boss1 boss1 = collision.gameObject.GetComponent<Boss1>();
            if (boss1) boss1.TakeDamage(damage);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController) playerController.TakeDamage(damage);

        }

    }

    public void TakeDamage(int damage, bool giveExperiance)
    {
        AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.hitRock);
        health -= damage;
        if (!gameObject.activeInHierarchy) return;

        if (health > 0)
        {
            flashWhite.Flash();
        }

        else if (health <= 0)
        {
            GameObject destroyEffect = objectPooler.GetPoolObject();
            destroyEffect.transform.position = transform.position;
            destroyEffect.transform.rotation = transform.rotation;
            destroyEffect.SetActive(true);
            AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.boom2);
            flashWhite.Reset();
            gameObject.SetActive(false);
            if (giveExperiance) PlayerController.Instance.GetExperiance(experianceToGive);
        }
    }
}
