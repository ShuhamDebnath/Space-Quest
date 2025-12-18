using Unity.Burst.Intrinsics;
using UnityEngine;

public class Critter1 : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;

    private float moveSpeed;
    private Vector3 tergetPosition;

    [SerializeField] private float moveTimer;
    [SerializeField] private float moveInterval;

    private Quaternion targetRotation;

    [SerializeField] private GameObject zappedEffect;
    [SerializeField] private GameObject BurnEffect;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        GenerateRandomPosition();
        moveInterval = Random.Range(0.3f, 2f);
        moveSpeed = Random.Range(0.3f, 3f);
        moveTimer = moveInterval;

    }
    void Update()
    {
        // if(moveTimer >  0)
        // {
        //     moveTimer -= Time.deltaTime;
        // }
        // else
        // {
        //     GenerateRandomPosition();
        //     moveInterval = Random.Range(0.3f, 3f);
        //     moveSpeed = Random.Range(0.3f, 2f);
        //     moveTimer = moveInterval;
        // }

        if (transform.position == tergetPosition)
        {
            moveSpeed = Random.Range(0.3f, 2f);
            GenerateRandomPosition();
        }

        Vector3 relativePostion = tergetPosition - transform.position;

        if (relativePostion != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(Vector3.forward, relativePostion);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * 3 * Time.deltaTime);
        }


        transform.position = Vector3.MoveTowards(transform.position, tergetPosition, moveSpeed * Time.deltaTime);


        float moveX = GameManager.Instance.worldSpeed * Time.deltaTime;
        transform.position += new Vector3(-moveX, 0);


        if (Mathf.Abs(transform.position.x) > 15 || Mathf.Abs(transform.position.y) > 5)
        {
            Destroy(gameObject);
        }

    }

    public void GenerateRandomPosition()
    {
        float moveX = Random.Range(-8f, 8f);
        float moveY = Random.Range(-4f, 4f);
        tergetPosition = new Vector3(moveX, moveY);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Instantiate(zappedEffect, transform.position, transform.rotation);
            AudioManager.Instance.PlaySound(AudioManager.Instance.squished);
            GameManager.Instance.critterCount++;

        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Instantiate(BurnEffect, transform.position, transform.rotation);
            AudioManager.Instance.PlaySound(AudioManager.Instance.Burn);
            GameManager.Instance.critterCount++;
        }
    }




}
