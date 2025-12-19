using UnityEngine;

public class PhaserWeapon : Weapon
{

    public static PhaserWeapon Instance;


    [SerializeField] private ObjectPooler bulletPool;


    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    public void Shoot()
    {

        for (int i = 0; i < stats[weaponLevel].amount; i++)
        {
            GameObject bullet = bulletPool.GetPoolObject();
            float YPos = transform.position.y;
            if (stats[weaponLevel].amount > 1)
            {
                float spacing = stats[weaponLevel].range / (stats[weaponLevel].amount - 1);
                YPos = transform.position.y - (stats[weaponLevel].range / 2) + i * spacing;
            }

            bullet.transform.position = new Vector2(transform.position.x, YPos);
            bullet.transform.localScale = new Vector2(stats[weaponLevel].size, stats[weaponLevel].size);
            bullet.SetActive(true);
            AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.shoot);
        }
    }

    public void LevelUp()
    {
        if(weaponLevel < stats.Count - 1)
        {
            weaponLevel++;
        }
    }

}
