using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PathFinding))]
public class BasicEnemy : Enemy
{
    [Header("Enemy Configuration")]
    [SerializeField] private EnemyType enemyType;

    [HideInInspector] public bool isFreezer;
    [HideInInspector] public int level;
    [HideInInspector] public EnemyStat publicEnemyStat;

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

        enemyStat = enemy.GetStat();

        publicEnemyStat = enemyStat;

        enemyStat.ShowStat();
    }

    void Update()
    {
        publicEnemyStat = enemyStat;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("TowerProjectile"))
        {
            Projectile projectile = other.gameObject.GetComponent<Projectile>();

            if(projectile != null)
                enemy.GetAttack(projectile.GetDamage());

            Destroy(other.gameObject);

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

    public void Freezer()
    {
        float temp = enemyStat.fireRate;

        if(isFreezer)
            enemyStat.fireRate = temp * 0.9f;
        else
            enemyStat.fireRate = temp;
    }

    public EnemyStat ReturnStat()
    {
        return enemyStat;
    }
}
