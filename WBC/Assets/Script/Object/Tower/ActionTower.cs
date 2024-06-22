using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BasicTower))]
public class ActionTower : Tower, ITowerStatObserver
{
    [Header("Tower")]    
    [SerializeField] private Tower action;

    private BasicTower basicTower;
    private float fireCooldown;
    private float fireTimer;

    private void Start()
    {
        basicTower = GetComponent<BasicTower>();

        basicTower.RegisterObserver(this);

        // 초기 스탯을 기반으로 fireCooldown 설정
        UpdateFireCooldown();
    }

    private void Update()
    {
        towerStat = basicTower.publicTowerStat;

        fireTimer += Time.deltaTime;

        if (fireTimer >= fireCooldown)
        {
            action.Action();
            fireTimer = 0f;
        }
        else if(towerStat.fireRate == 0)
            action.Action();
    }

    public void OnTowerStatChanged(TowerStat newStat)
    {
        towerStat = newStat;

        UpdateFireCooldown();
    }

    private void UpdateFireCooldown()
    {
        fireCooldown = 1f / towerStat.fireRate;
    }
}
