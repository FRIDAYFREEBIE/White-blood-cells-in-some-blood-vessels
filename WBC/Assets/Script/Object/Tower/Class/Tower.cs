using UnityEngine;

public struct TowerStat
{
    public float hp;
    public int level;
    public float range;
    public float ability;
    public float fireRate;
    public int price;
    public TowerType towerType;

    public TowerStat(float hp, int level, float range, float ability, float fireRate, int price, TowerType towerType)
    {
        this.hp = hp;
        this.level = level;
        this.range = range;
        this.ability = ability;
        this.fireRate = fireRate;  // 연사력 초기화
        this.price = price;
        this.towerType = towerType;
    }

    public TowerStat Upgrade()
    {
        return new TowerStat(hp * 2, level + 1, range * 1.5f, ability * 2, fireRate * 2, price, towerType);
    }

    public void ShowStat()
    {
        Debug.Log("Tower Stat\n" + "hp: " + hp + " level: " + level + " range: " + range + " ability: " + ability + " fireRate: " + fireRate + " towerType: " + towerType);
    }
}


public enum TowerType
{
    Scout,
    Shotgun,
    Ranger,
    Railgun,
    Farm,
    Commander,
    Freezer
}

public enum TowerState
{
    Activate,
    Disabled
}

public class Tower : MonoBehaviour, ITower
{
    protected TowerStat towerStat;
    protected TowerState towerState;

    public void Initialize(TowerStat towerStat)
    {
        this.towerStat = towerStat;
    }

    public TowerStat GetStat()
    {
        return towerStat;
    }

    public TowerState GetState()
    {
      return towerState;
    }

    public void GetAttack(float damage)
    {
        towerStat.hp -= damage;
    }

    public virtual void Action()
    {
        // 추상 메서드
    }

    public void UpdateState(TowerState towerState)
    {
        this.towerState = towerState;
    }

    public TowerStat UpgradeTower()
    {
        towerStat = towerStat.Upgrade();
        return towerStat;
    }
}

public static class TowerFactory
{
    public static TowerStat CreateTowerStat(TowerType towerType)
    {
        switch (towerType)
        {
            case TowerType.Scout:
                return new TowerStat(10, 1, 5, 1, 1, 50, towerType);  // 연사력 추가
            case TowerType.Shotgun:
                return new TowerStat(10, 1, 5, 1, 0.5f, 100, towerType);
            case TowerType.Ranger:
                return new TowerStat(10, 1, 10, 2, 0.8f, 200, towerType);
            case TowerType.Railgun:
                return new TowerStat(100000, 1, 100, 1000, 0, 100000, towerType);
            case TowerType.Farm:
                return new TowerStat(20, 1, 100, 10, 0.1f, 500, towerType);
            case TowerType.Commander:
                return new TowerStat(20, 1, 15, 10, 0, 500, towerType);
            case TowerType.Freezer:
                return new TowerStat(20, 1, 15, 10, 0, 500, towerType);
            default:
                return new TowerStat(); // 기본값으로 초기화
        }
    }

    public static Tower CreateTower(MonoBehaviour behaviour, TowerType towerType)
    {
        TowerStat towerStat = CreateTowerStat(towerType);
        Tower tower = behaviour.gameObject.AddComponent<Tower>();
        tower.Initialize(towerStat);
        return tower;
    }
}
