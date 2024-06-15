using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFinding))]
public class BasicEnemy : Enemy
{
    [Header("Enemy Configuration")]
    [SerializeField] private EnemyType enemyType;

    private int level;

    private Enemy enemy;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        level = gameManager.CurrentStage();

        enemy = EnemyFactory.CreateEnemy(this, enemyType, level);

        enemy.GetStat().ShowStat();

        AdjustScaleBasedOnLevel();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("TowerProjectile"))
        {
            Projectile projectile = other.gameObject.GetComponent<Projectile>();

            enemy.GetAttack(projectile.GetDamage());

            if(enemyStat.hp <= 0)
                Die();
        }
    }

    public void Die()
    {
        enemyState = EnemyState.Die;

        gameManager.ChangeMoney(enemyStat.money);
    }

    private void AdjustScaleBasedOnLevel()
    {
        float baseScaleMultiplier = 0.5f;

        float scaleFactor = baseScaleMultiplier * level;

        transform.localScale = new Vector3(scaleFactor, scaleFactor, transform.localScale.z);
    }
}
