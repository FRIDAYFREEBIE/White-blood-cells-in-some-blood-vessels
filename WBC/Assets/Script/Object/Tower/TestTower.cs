using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTower : Tower
{
    [Header("towerType")]
    [SerializeField] private TowerType towerType;

    private Tower tower;

    void Start()
    {
        tower = TowerFactory.CreateTower(this, towerType);
        tower.GetStat().ShowStat();
    }
}