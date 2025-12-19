using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int damage;
    [SerializeField] private int experianceToGive;

    protected SpriteRenderer spriteRenderer;
    private FlashWhite flashWhite;
    protected ObjectPooler objectPooler;
    protected AudioSource hitSound;
    protected AudioSource distroySound;

    protected float speedX = 0;
    protected float speedY = 0;


    void OnEnable()
    {
        health = maxHealth;
    }
    public virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        flashWhite = GetComponent<FlashWhite>();

    }

    void Update()
    {

        transform.position += new Vector3(speedX * Time.deltaTime, speedY * Time.deltaTime);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController) playerController.TakeDamage(damage);

        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        AudioManager.Instance.PlayModifiedSound(hitSound);
        if (health > 0)
        {
            flashWhite.Flash();

        }
        else
        {

            flashWhite.Reset();
            AudioManager.Instance.PlayModifiedSound(distroySound);
            GameObject destroyEffect = objectPooler.GetPoolObject();
            destroyEffect.transform.position = transform.position;
            destroyEffect.transform.rotation = transform.rotation;
            destroyEffect.SetActive(true);

            PlayerController.Instance.GetExperiance(experianceToGive);


            gameObject.SetActive(false);
        }
    }
}
