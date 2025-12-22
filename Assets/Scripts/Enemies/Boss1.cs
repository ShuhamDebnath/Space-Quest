using UnityEngine;

public class Boss1 : Enemy
{
    public static Boss1 Instance;

    private Animator animator;

    private bool charging;

    private float switchInteval;
    private float switchTimer;


    private ObjectPooler distroyEffectPool;




    public override void Awake()
    {
        base.Awake();
        if (Instance != null) Destroy(Instance);
        else Instance = this;

        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        health = maxHealth;
        EnterChargeState();
        AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.bossSpawn);
        
    }

    public override void Start()
    {
        base.Start();
        distroyEffectPool = GameObject.Find("Boom1Pool").GetComponent<ObjectPooler>();

        objectPooler = GameObject.Find("BettlePopPool").GetComponent<ObjectPooler>();
        hitSound = AudioManager.Instance.BossHit;
        distroySound = AudioManager.Instance.boom2;
        speedX = Random.Range(-0.8f, -1.5f);
        EnterChargeState();

    }

    public override void Update()
    {
        base.Update();

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

        if (transform.position.x < -12) gameObject.SetActive(false);



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
        speedX = -5f;
        speedY = 0;
        switchInteval = Random.Range(0.3f, 1.5f);
        switchTimer = switchInteval;
        charging = true;
        animator.SetBool("charging", charging);
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
