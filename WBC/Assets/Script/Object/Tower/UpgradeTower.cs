using System;
using UnityEngine;

[RequireComponent(typeof(Tower))]

public class UpgradeTower : MonoBehaviour
{

    [Header("Upgrade Multiplier")]
    [SerializeField, Range(0,10)] private float upgradeMultiplier;

    private Tower tower;
    private TowerStat towerStat;

    private void Start()
    {
        tower  = GetComponent<Tower>();
    }

    public void OnClickUpgradeButton()
    {
        towerStat = tower.UpgradeTower(upgradeMultiplier);

        Debug.Log("Tower upgraded! upgradeMultiplier: " + upgradeMultiplier);
        towerStat.ShowStat();
    }
}