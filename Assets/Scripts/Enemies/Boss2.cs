using UnityEngine;

public class Boss2 : Enemy
{


    public static Boss2 Instance;

    private Animator animator;

    private bool charging = true;

    private float switchInteval;
    private float switchTimer;


    private ObjectPooler distroyEffectPool;

    public override void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();

    }

    public override void OnEnable()
    {
        base.OnEnable();
        //health = maxHealth;
        EnterIdleState();
        //AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.bossSpawn);

    }

    public override void Start()
    {
        base.Start();
        distroyEffectPool = GameObject.Find("Boom1Pool").GetComponent<ObjectPooler>();

        //objectPooler = GameObject.Find("BettlePopPool").GetComponent<ObjectPooler>();
        hitSound = AudioManager.Instance.BossHit;
        distroySound = AudioManager.Instance.boom2;
        //speedX = Random.Range(-0.8f, -1.5f);
        //EnterChargeState();

    }

    public override void Update()
    {
        base.Update();

        float playerPosition = PlayerController.Instance.transform.position.x;

        //Vector3 playerPosition = PlayerController.Instance.transform.position;

        // if (switchTimer > 0) switchTimer -= Time.deltaTime;
        // else
        // {
        //     if (charging && transform.position.x > playerPosition.x) EnterPatrolState();
        //     else EnterChargeState();
        // }

        if (Mathf.Abs(transform.position.y) > 4) speedY *= -1;




        if (transform.position.x > 7.5)
        {
            EnterIdleState();
        }
        else if (transform.position.x < -7 || transform.position.x < playerPosition)
        {
            EnterChargeState();
        }


        // bool playerBoost = PlayerController.Instance.isBoosting;
        // float moveX;
        // if (playerBoost && !charging) moveX = GameManager.Instance.worldSpeed * Time.deltaTime * -0.5f;
        // else moveX = speedX * Time.deltaTime;
        // //float moveX = speedX * GameManager.Instance.worldSpeed  * Time.deltaTime;
        // float moveY = speedY * Time.deltaTime;


        // transform.position += new Vector3(-moveX, moveY);

        // if (transform.position.x < -12) gameObject.SetActive(false);





    }

    void EnterIdleState()
    {
        if (charging)
        {


            //speedX = GameManager.Instance.worldSpeed;
            speedX = 0.2f;
            speedY = Random.Range(-2f, 2f);
            switchInteval = Random.Range(5f, 10f);
            //switchTimer = switchInteval;
            charging = false;
            animator.SetBool("charging", charging);
        }
    }

    void EnterChargeState()
    {
        if (!charging)
        {
            AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.BossCharge);
            speedX = Random.Range(3.5f, 4f);
            speedY = 0;
            //switchInteval = Random.Range(0.3f, 1.5f);
            //switchTimer = switchInteval;
            charging = true;
            animator.SetBool("charging", charging);
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        health -= damage;
        AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.BossHit);
        if (health <= 0)
        {

            GameObject destroyEffect = distroyEffectPool.GetPoolObject();
            destroyEffect.transform.position = transform.position;
            destroyEffect.transform.rotation = transform.rotation;
            destroyEffect.SetActive(true);
            AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.boom2);
            gameObject.SetActive(false);

            PlayerController.Instance.GetExperiance(experianceToGive);
        }
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Astroid astroid = collision.gameObject.GetComponent<Astroid>();
            if (astroid) astroid.TakeDamage(damage, false);
        }

    }
}
