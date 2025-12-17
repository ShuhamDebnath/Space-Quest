using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private Rigidbody2D rb;
    private Animator animator;
    public float boost = 1f;
    private float boostPower = 5f;
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
     private Material defaultMaterial;
     [SerializeField]private Material whiteMaterial;
    


    void Awake()
    {
        if(Instance!= null)
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

        defaultMaterial = spriteRenderer.material;

        

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

        if(Time.timeScale > 0)
        { 
            animator.SetFloat("moveX", directionX);
            animator.SetFloat("moveY", directionY);
        

            if(Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire2"))
            {
                EnterBoost();
            }
            else if(Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire2"))
            {
                ExitBoost();
            }
        }
        
    }

    void FixedUpdate()
    {
        rb.linearVelocity = playerDirection * moveSpeed;

        if(isBoosting)
        {
            if(energy >= 0.2f) energy -= 0.2f;
            else ExitBoost();
        }
        else
        {
            if(energy < maxEnergy) energy += regenEnergy;
        }

        UIController.Instance.UpdateEnergySlider(energy, maxEnergy);
    }

    void EnterBoost()
    {
        
        if(energy > 5)
        {
            animator.SetBool("boosting", true);
            boost = boostPower;
            isBoosting = true; 
            AudioManager.Instance.PlaySound(AudioManager.Instance.fire);
        }
        
    }

    public void ExitBoost()
    {
        animator.SetBool("boosting", false);
        boost = 1f;
        isBoosting = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage(1);
            
        }
        
    }

    private void TakeDamage(int damage)
    {
        
        health = health - damage;
        UIController.Instance.UpdateHealthSlider(health,maxHealth);
        AudioManager.Instance.PlaySound(AudioManager.Instance.hit);
        spriteRenderer.material = whiteMaterial;
        StartCoroutine(ResetMaterial());

        if(health <= 0)
        {
            boost = 0f;
            gameObject.SetActive(false);
            Instantiate(destroyEffect, transform.position, transform.rotation);
            GameManager.Instance.GameOver();
            AudioManager.Instance.PlaySound(AudioManager.Instance.ice);
        }
    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(.2f);
        spriteRenderer.material = defaultMaterial;
        
    }



}
