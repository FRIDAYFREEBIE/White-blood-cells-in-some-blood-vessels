using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTower : Tower
{
    [Header("towerType")]
    [SerializeField] private TowerType towerType;

    void Start()
    {
        Tower tower = TowerFactory.CreateTower(this, towerType);
        tower.towerStat.ShowStat();
    }
}