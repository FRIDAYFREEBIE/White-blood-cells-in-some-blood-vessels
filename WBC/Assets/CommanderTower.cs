using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderTower : Tower, ITowerStatObserver
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

        List<Tower> detectedTower = new List<Tower>();

        foreach (var collider in hitColliders)
        {
            Tower tower = collider.GetComponent<Tower>();
            if (tower != null)
            {
                detectedTower.Add(tower);

                BasicTower basicTower = tower.GetComponent<BasicTower>();
                if (basicTower != null)
                {
                    basicTower.isCommander = true;
                }
            }
        }
    }

    public void OnTowerStatChanged(TowerStat newStat)
    {
        towerStat = newStat;
    }
}
