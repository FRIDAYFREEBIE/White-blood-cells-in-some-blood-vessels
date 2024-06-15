using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PathFinding))]
public class BasicEnemy : Enemy
{
    [Header("Enemy Configuration")]
    [SerializeField] private EnemyType enemyType;

    private int level;

    private Enemy enemy;
    private GameManager gameManager;
    private EnemySpawner enemySpawner;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();

        if(gameManager.CurrentStage() > 5)
            level = gameManager.CurrentStage()/5 + 1;
        else
            level = 1;

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

            GameObject.Destroy(other.gameObject);

            if(enemyStat.hp <= 0)
                Die();
        }
    }

    public void Die()
    {
        enemyState = EnemyState.Die;

        gameManager.ChangeMoney(enemyStat.money);

        enemySpawner.RemoveEnemy(this);

        GameObject.Destroy(this);
    }

    private void AdjustScaleBasedOnLevel()
    {
        float baseScaleMultiplier = 0.5f;

        float scaleFactor = baseScaleMultiplier * level;

        transform.localScale = new Vector3(scaleFactor, scaleFactor, transform.localScale.z);
    }
}
