using UnityEngine;

[RequireComponent(typeof(BasicTower))]
public class UpgradeTower : Tower, ITowerStatObserver
{
    [Header("Upgrade Multiplier")]
    [SerializeField, Range(0, 10)] private float upgradeMultiplier;

    private BasicTower basicTower;

    private void Start()
    {
        basicTower = GetComponent<BasicTower>();

        basicTower.RegisterObserver(this);
    }

    public void OnClickUpgradeButton()
    {
        towerStat = basicTower.UpgradeTower(upgradeMultiplier);

        basicTower.UpdateTowerStat(towerStat);
    }

    public void OnTowerStatChanged(TowerStat newStat)
    {
        towerStat = newStat; 

        towerStat.ShowStat();
    }
}
