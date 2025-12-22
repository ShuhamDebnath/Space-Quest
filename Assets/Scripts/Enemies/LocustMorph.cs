using System.Collections.Generic;
using UnityEngine;

public class LocustMorph : Enemy
{

    [SerializeField] private List<Frame> frames;
    private int locustIndex;
    private bool charging;

    [System.Serializable]
    private class Frame
    {
        public List<Sprite> sprites;
        
    }

    public override void OnEnable()
    {
        base.OnEnable();
        locustIndex = Random.Range(0, frames.Count);
        EnterIdle();

    }


    public override void Start()
    {
        base.Start();
        objectPooler = GameObject.Find("LocustPopPool").GetComponent<ObjectPooler>();
        hitSound = AudioManager.Instance.locustHit;
        distroySound = AudioManager.Instance.locustDestroy;
        EnterIdle();
    }

    public override void Update()
    {
        base.Update();

        if(Mathf.Abs(transform.position.y ) > 5) speedY *= -1;

    }

    public override void TakeDamage(int damage)
    {

        base.TakeDamage(damage);
        if(health < maxHealth * 0.5)
        {
            EnterCharge();
            
        }
    }

    private void EnterIdle()
    {
        charging = false;
        locustIndex = Random.Range(0, frames.Count);
        spriteRenderer.sprite = frames[locustIndex].sprites[0];
        speedX = Random.Range(0.1f, 0.6f);
        speedY = Random.Range(-0.9f, 0.9f);
        
    } 
    private void EnterCharge()
    {
        if (!charging)
        {
            charging = true;
            spriteRenderer.sprite = frames[locustIndex].sprites[1];
            AudioManager.Instance.PlaySound(AudioManager.Instance.locustCharge);
            speedX = Random.Range(-4f, -6f);
            speedY = 0;
        }
        
    } 

}
