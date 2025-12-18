using System;
using System.Collections;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private Rigidbody2D rb;
    private Animator animator;
    public bool isBoosting = false;

    [SerializeField] private Vector2 playerDirection;
    [SerializeField] private float moveSpeed;

    [SerializeField] private float energy;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float regenEnergy;

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private float regenHealth;

    [SerializeField] private GameObject destroyEffect;

    private SpriteRenderer spriteRenderer;
    private FlashWhite flashWhite;

    [SerializeField] private ParticleSystem engineEffect;
    public int damage;



    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    void Start()
    {

        //moveSpeed = 2;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //preFab = GetComponent<GameObject>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        flashWhite = GetComponent<FlashWhite>();

        GameManager.Instance.SetWorldSpeed(1f);

        damage = 5;

        energy = maxEnergy;
        health = maxHealth;

        UIController.Instance.UpdateEnergySlider(energy, maxEnergy);
        UIController.Instance.UpdateHealthSlider(health, maxHealth);

    }

    void Update()
    {
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");
        playerDirection = new Vector2(directionX, directionY).normalized;

        if (Time.timeScale > 0)
        {
            animator.SetFloat("moveX", directionX);
            animator.SetFloat("moveY", directionY);


            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire2"))
            {
                EnterBoost();
            }
            else if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire2"))
            {
                ExitBoost();
            }


            if (Input.GetButtonDown("Fire1"))
            {
                PhaserWeapon.Instance.Shoot();
            }
        }


    }

    void FixedUpdate()
    {
        rb.linearVelocity = playerDirection * moveSpeed;

        if (isBoosting)
        {
            if (energy >= 0.5f) energy -= 0.5f;
            else ExitBoost();
        }
        else
        {
            if (energy < maxEnergy) energy += regenEnergy;
        }

        UIController.Instance.UpdateEnergySlider(energy, maxEnergy);
    }

    void EnterBoost()
    {

        if (energy > 5)
        {
            animator.SetBool("boosting", true);
            GameManager.Instance.SetWorldSpeed(5f);
            isBoosting = true;
            AudioManager.Instance.PlaySound(AudioManager.Instance.fire);
            engineEffect.Play();
        }

    }

    public void ExitBoost()
    {
        animator.SetBool("boosting", false);
        GameManager.Instance.SetWorldSpeed(1f);
        isBoosting = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Astroid astroid = collision.gameObject.GetComponent<Astroid>();
            if (astroid) astroid.TakeDamage(damage);
            
        }
        else if (collision.gameObject.CompareTag("Critter"))
        {
            Critter1 critter1 = collision.gameObject.GetComponent<Critter1>();
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            Boss1 boss1 = collision.gameObject.GetComponent<Boss1>();
            if (boss1) boss1.TakeDamage(damage);
            gameObject.SetActive(false);
        }

    }

    public void TakeDamage(int damage)
    {

        health = health - damage;
        UIController.Instance.UpdateHealthSlider(health, maxHealth);
        AudioManager.Instance.PlaySound(AudioManager.Instance.hit);
        flashWhite.Flash();

        if (health <= 0)
        {
            GameManager.Instance.SetWorldSpeed(0f);
            gameObject.SetActive(false);
            Instantiate(destroyEffect, transform.position, transform.rotation);
            GameManager.Instance.GameOver();
            AudioManager.Instance.PlaySound(AudioManager.Instance.ice);
        }
    }



}
