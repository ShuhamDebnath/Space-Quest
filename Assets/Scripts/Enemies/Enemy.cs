using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int damage;
    [SerializeField] protected int experianceToGive;

    protected SpriteRenderer spriteRenderer;
    private FlashWhite flashWhite;
    protected ObjectPooler objectPooler;
    protected AudioSource hitSound;
    protected AudioSource distroySound;

    protected float speedX = 0;
    protected float speedY = 0;

    public virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void OnEnable()
    {
        health = maxHealth;
    }
    public virtual void Start()
    {
        flashWhite = GetComponent<FlashWhite>();

    }

    public virtual void Update()
    {

        transform.position += new Vector3(speedX * Time.deltaTime, speedY * Time.deltaTime);

    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController) playerController.TakeDamage(damage);

        }
    }

    public virtual void TakeDamage(int damage)
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
