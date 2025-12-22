using UnityEngine;

public class Bettlemorphs : Enemy
{
    [SerializeField] private Sprite[] sprites;
    private float timer;
    private float frequency;
    private float amplitude;
    private float centerY;

    public override void OnEnable()
    {
        base.Start();
        centerY = transform.position.y;
        timer = centerY;
        frequency = Random.Range(0.3f, 1f);
        amplitude = Random.Range(0.8f, 1.5f);
        
        
    }

    public override void Start()
    {
        base.Start();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        objectPooler = GameObject.Find("BettlePopPool").GetComponent<ObjectPooler>();
        hitSound = AudioManager.Instance.bettleHit;
        distroySound = AudioManager.Instance.bettleDestroy;
        speedX = Random.Range(-0.8f, -1.5f);
    }

     public override void Update()
    {
        base.Update();

        timer -= Time.deltaTime;
        float sine = Mathf.Sin(timer * frequency) * amplitude;
        transform.position = new Vector3(transform.position.x, sine + centerY);
        
    }


}
