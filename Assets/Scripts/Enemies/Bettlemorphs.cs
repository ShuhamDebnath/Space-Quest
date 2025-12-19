using UnityEngine;

public class Bettlemorphs : Enemy
{
    [SerializeField] private Sprite[] sprites;

    public override void Start()
    {
        base.Start();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        objectPooler = GameObject.Find("BettlePopPool").GetComponent<ObjectPooler>();
        hitSound = AudioManager.Instance.bettleHit;
        distroySound = AudioManager.Instance.bettleDestroy;

        speedX = Random.Range(-0.8f, -1.5f);
    }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.boom2);
    // }


}
