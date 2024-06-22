using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmTower : Tower, ITowerStatObserver
{
    private GameManager gameManager;
    private BasicTower basicTower;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        basicTower = GetComponent<BasicTower>();

        basicTower.RegisterObserver(this);
    }

    public override void Action()
    {
        gameManager.ChangeMoney(+(int)basicTower.publicTowerStat.ability);
    }

    public void OnTowerStatChanged(TowerStat newStat)
    {
        towerStat = newStat;
    }
}
