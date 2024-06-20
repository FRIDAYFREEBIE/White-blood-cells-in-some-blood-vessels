using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunTower : Tower, ITowerStatObserver
{
    [Header("Projectile")]
    [SerializeField] private Projectile projectilePrefab;  // projectile 프리팹을 참조하도록 변경

    private BasicTower basicTower;

    void Start()
    {
        basicTower = GetComponent<BasicTower>();

        basicTower.RegisterObserver(this);
    }
    
    void Update()
    {
        towerStat = basicTower.publicTowerStat;
    }

    public override void Action()
    {
        float range = towerStat.range;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range);
        Enemy closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = hitCollider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                    if (distanceToEnemy < closestDistance)
                    {
                        closestDistance = distanceToEnemy;
                        closestEnemy = enemy;
                    }
                }
            }
        }

        if (closestEnemy != null)
        {
            float[] spreadAngles = { -60f, -30f, 0f, 30f, 60f };

            foreach (float angle in spreadAngles)
            {
                Projectile newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

                newProjectile.Initialize(towerStat.ability);

                Vector2 directionToEnemy = (closestEnemy.transform.position - transform.position).normalized;

                float angleInRadians = angle * Mathf.Deg2Rad;
                Vector2 spreadDirection = new Vector2(
                    directionToEnemy.x * Mathf.Cos(angleInRadians) - directionToEnemy.y * Mathf.Sin(angleInRadians),
                    directionToEnemy.x * Mathf.Sin(angleInRadians) + directionToEnemy.y * Mathf.Cos(angleInRadians)
                ).normalized;

                float projectileSpeed = 10f; // 발사체 속도를 원하는 값으로 설정
                newProjectile.GetComponent<Rigidbody2D>().velocity = spreadDirection * projectileSpeed;

                float angleInDegrees = Mathf.Atan2(spreadDirection.y, spreadDirection.x) * Mathf.Rad2Deg;
                newProjectile.transform.rotation = Quaternion.Euler(0, 0, angleInDegrees);
            }
        }
    }

    public void OnTowerStatChanged(TowerStat newStat)
    {
        towerStat = newStat;
    }
}
