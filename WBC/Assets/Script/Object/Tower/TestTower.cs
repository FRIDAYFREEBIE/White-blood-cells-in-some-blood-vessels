using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTower : Tower
{
    [Header("towerType")]
    [SerializeField] private TowerType towerType;

    void Start()
    {
        towerStat = SetStat(towerType);
    }

    public override void Action()
    {
        
    }
}
