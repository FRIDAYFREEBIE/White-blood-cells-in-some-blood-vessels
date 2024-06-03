using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{
    [Header("enemyType")]
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private int level = 1;

    void Start()
    {
        Enemy enemy = EnemyFactory.CreateEnemy(this, enemyType, level);

        enemy.GetStat().ShowStat();
    }

    void Update()
    {
      
    }
}
