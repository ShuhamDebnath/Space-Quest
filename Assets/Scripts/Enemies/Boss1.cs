using UnityEngine;

public class Boss1 : MonoBehaviour
{

    private Animator animator;

    private float speedX;
    private float speedY;
    private bool charging;

    private float switchInteval;
    private float switchTimer;

    private float lives;

    void Start()
    {
        animator = GetComponent<Animator>();
        EnterChargeState();
        lives = 50;

    }

    void Update()
    {
        if (switchTimer > 0) switchTimer -= Time.deltaTime;
        else
        {
            if (charging) EnterPatrolState();
            else EnterChargeState();
        }

        if (Mathf.Abs(transform.position.y) > 2) speedY *= -1;

        float moveX = speedX * GameManager.Instance.worldSpeed  * Time.deltaTime;
        float moveY = speedY * Time.deltaTime;


        transform.position += new Vector3(-moveX, moveY);
        if (transform.position.x < -11) Destroy(gameObject);

        if(transform.position.x < -15) TakeDamage(100);

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
        speedX = 4f;
        speedY = 0;
        switchInteval = Random.Range(2f, 2.5f);
        switchTimer = switchInteval;
        charging = true;
        animator.SetBool("charging", charging);
        AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.BossCharge);
    }

    public void TakeDamage(int damage)
    {
        lives -= damage;
        AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.BossHit);
        if(lives <= 0 ) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) TakeDamage(0);
    }


}
