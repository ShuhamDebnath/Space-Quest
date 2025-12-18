using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public static Boss1 Instance;

    private Animator animator;

    private float speedX;
    private float speedY;
    private bool charging;

    private float switchInteval;
    private float switchTimer;

    private int health;
    private int damage;

    void Awake()
    {
        if (Instance != null) Destroy(Instance);
        else Instance = this;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        EnterChargeState();
        health = 50;
        damage = 10;

    }

    void Update()
    {

        Vector3 playerPosition = PlayerController.Instance.transform.position;

        if (switchTimer > 0) switchTimer -= Time.deltaTime;
        else
        {
            if (charging && transform.position.x > playerPosition.x) EnterPatrolState();
            else EnterChargeState();
        }

        if (Mathf.Abs(transform.position.y) > 2) speedY *= -1;
        else if (transform.position.x < playerPosition.x) EnterChargeState();


        bool playerBoost = PlayerController.Instance.isBoosting;
        float moveX;
        if (playerBoost && !charging) moveX = GameManager.Instance.worldSpeed * Time.deltaTime * -0.5f;
        else moveX = speedX * Time.deltaTime;
        //float moveX = speedX * GameManager.Instance.worldSpeed  * Time.deltaTime;
        float moveY = speedY * Time.deltaTime;


        transform.position += new Vector3(-moveX, moveY);
        if (transform.position.x < -11) Destroy(gameObject);

        if (transform.position.x < -15) TakeDamage(100);



    }

    void EnterPatrolState()
    {
        speedX = GameManager.Instance.worldSpeed;
        speedY = Random.Range(-2f, 2f);
        switchInteval = Random.Range(5f, 10f);
        switchTimer = switchInteval;
        charging = false;
        animator.SetBool("charging", charging);
    }

    void EnterChargeState()
    {
        if (!charging) AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.BossCharge);
        speedX = 10f;
        speedY = 0;
        switchInteval = Random.Range(0.3f, 1.5f);
        switchTimer = switchInteval;
        charging = true;
        animator.SetBool("charging", charging);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.BossHit);
        if (health <= 0) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Astroid astroid = collision.gameObject.GetComponent<Astroid>();
            if (astroid) astroid.TakeDamage(damage);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController) playerController.TakeDamage(damage);

        }

    }


}
