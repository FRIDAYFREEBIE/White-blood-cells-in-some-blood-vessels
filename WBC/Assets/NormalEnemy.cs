using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy
{
    [Header("Projectile")]
    [SerializeField] private Projectile projectilePrefab;

    private BasicEnemy basicEnemy;

    void Start()
    {
        basicEnemy = GetComponent<BasicEnemy>();
    }

    void Update()
    {
        enemyStat = basicEnemy.publicEnemyStat;
    }

    public override void Action()
    {
        float range = enemyStat.range;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range);
        GameObject closestTower = null;
        float closestDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Tower"))
            {
                Tower tower = hitCollider.GetComponent<Tower>();
                if (tower != null)
                {
                    float distanceToTower = Vector2.Distance(transform.position, tower.transform.position);
                    if (distanceToTower < closestDistance)
                    {
                        closestDistance = distanceToTower;
                        closestTower = tower.gameObject;
                    }
                }
            }
        }

        if (closestTower != null)
        {
            Projectile newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            newProjectile.Initialize(enemyStat.damage);
            newProjectile.SetTarget(closestTower);
        }
    }
}
