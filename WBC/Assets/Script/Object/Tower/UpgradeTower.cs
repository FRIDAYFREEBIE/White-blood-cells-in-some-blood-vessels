using UnityEngine;

[RequireComponent(typeof(BasicTower))]
public class UpgradeTower : Tower, ITowerStatObserver
{
    [Header("Upgrade Multiplier")]
    [SerializeField, Range(0, 10)] private float upgradeMultiplier;

    private GameManager gameManager;
    private BasicTower basicTower;

    private void Start()
    {
        basicTower = GetComponent<BasicTower>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        basicTower.RegisterObserver(this);
    }

    public void OnClickUpgradeButton()
    {
        int price = towerStat.price*(towerStat.level+1);

        if(gameManager.CurrentMoney() >= price)
        {
            towerStat = basicTower.UpgradeTower(upgradeMultiplier);

            towerStat.ShowStat();

            gameManager.ChangeMoney(-price);
        }
    }

    public void OnTowerStatChanged(TowerStat newStat)
    {
        towerStat = newStat; 

        towerStat.ShowStat();
    }
}
