using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PathFinding))]
public class BasicEnemy : Enemy
{
    [Header("Enemy Configuration")]
    [SerializeField] private EnemyType enemyType;

    public int level;

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

        enemy.GetStat();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("TowerProjectile"))
        {
            Projectile projectile = other.gameObject.GetComponent<Projectile>();

            if(projectile != null)
                enemy.GetAttack(projectile.GetDamage());

            GameObject.Destroy(other.gameObject);

            if(enemyStat.hp <= 0)
                Die();
        }
    }

    public void Die()
    {
        enemyState = EnemyState.Die;

        gameManager.ChangeMoney(enemyStat.money*enemyStat.level);

        enemySpawner.RemoveEnemy(this);

        GameObject.Destroy(this);
    }

    public EnemyStat ReturnStat()
    {
        return enemyStat;
    }
}
