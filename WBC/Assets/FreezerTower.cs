using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerTower : Tower, ITowerStatObserver
{
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
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, towerStat.range);

        List<Enemy> detectedEnemies = new List<Enemy>();

        foreach (var collider in hitColliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                detectedEnemies.Add(enemy);

                BasicEnemy basicEnemy = enemy.GetComponent<BasicEnemy>();
                if (basicEnemy != null)
                {
                    basicEnemy.isFreezer = true;
                }
            }
        }
    }

    public void OnTowerStatChanged(TowerStat newStat)
    {
        towerStat = newStat;
    }
}