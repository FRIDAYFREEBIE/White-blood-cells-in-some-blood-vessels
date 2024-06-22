using UnityEngine;

[RequireComponent(typeof(BasicTower))]
public class UpgradeTower : Tower, ITowerStatObserver
{
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
            towerStat = basicTower.UpgradeTower();

            towerStat.ShowStat();

            gameManager.ChangeMoney(-price);
        }
    }

    public void OnTowerStatChanged(TowerStat newStat)
    {
        towerStat = newStat; 
    }
}
