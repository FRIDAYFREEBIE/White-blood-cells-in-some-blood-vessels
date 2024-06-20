using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutTower : Tower, ITowerStatObserver
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
            Projectile newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            newProjectile.Initialize(towerStat.ability);
            newProjectile.SetTarget(closestEnemy);
        }
    }

    public void OnTowerStatChanged(TowerStat newStat)
    {
        towerStat = newStat;
    }
}
