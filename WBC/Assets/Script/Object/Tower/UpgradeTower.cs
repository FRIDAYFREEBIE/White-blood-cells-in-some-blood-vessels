using System;
using UnityEngine;

[RequireComponent(typeof(Tower))]

public class UpgradeTower : MonoBehaviour
{
    [Header("Each Tower")]
    [SerializeField] private Tower tower;

    [Header("Upgrade Multiplier")]
    [SerializeField, Range(0,10)] private float upgradeMultiplier;

    private TowerStat towerStat;

    private void Start()
    {
        if(tower == null)
            tower  = GetComponent<Tower>();
    }

    public void OnClickUpgradeButton()
    {
        towerStat = tower.UpgradeTower(upgradeMultiplier);

        Debug.Log("Tower upgraded: ");
        towerStat.ShowStat();
    }
}