using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{
    [Header("enemyType")]
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private int level = 1;

    Enemy enemy;

    void Start()
    {
        enemy = EnemyFactory.CreateEnemy(this, enemyType, level);

        enemy.GetStat().ShowStat();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("TowerProjectile"))
        {
            Projectile projectile = other.gameObject.GetComponent<Projectile>();

            enemy.GetAttack(projectile.GetDamage());
        }
    }
}
