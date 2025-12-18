using UnityEngine;

public class PhaserWeapon : MonoBehaviour
{

    public static PhaserWeapon Instance;

    //[SerializeField] private GameObject preFab;
    [SerializeField] private ObjectPoller bulletPool;

    public float speed;
    public int damage;

    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }


    void Start()
    {
        speed = 10;
        damage = 2;

    }

    void Update()
    {

    }

    public void Shoot()
    {
        GameObject bullet = bulletPool.GetPoolObject();
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
        AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.shoot);

    }
}
