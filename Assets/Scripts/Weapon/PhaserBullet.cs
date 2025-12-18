using UnityEngine;

public class PhaserBullet : MonoBehaviour
{

    void Update()
    {
        transform.position += new Vector3(PhaserWeapon.Instance.speed * Time.deltaTime, 0f, 0f);
        if (transform.position.x > 11) gameObject.SetActive(false);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Astroid astroid = collision.gameObject.GetComponent<Astroid>();
            if (astroid) astroid.TakeDamage(PhaserWeapon.Instance.damage);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Critter"))
        {
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            Boss1 boss1 = collision.gameObject.GetComponent<Boss1>();
            if (boss1) boss1.TakeDamage(PhaserWeapon.Instance.damage);
            gameObject.SetActive(false);
        }

    }
}

