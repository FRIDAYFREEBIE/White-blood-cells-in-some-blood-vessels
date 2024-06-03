using UnityEngine;

public struct TowerStat
{
    public float hp;
    public int level;
    public float range;
    public float ability;
    public TowerType towerType;

    public TowerStat(float hp, int level, float range, float ability, TowerType towerType)
    {
        this.hp = hp;
        this.level = level;
        this.range = range;
        this.ability = ability;
        this.towerType = towerType;
    }

    public TowerStat Upgrade(float upgradeMultiplier)
    {
        return new TowerStat(hp * upgradeMultiplier, level + 1, range * upgradeMultiplier, ability * upgradeMultiplier, towerType);
    }

    public void ShowStat()
    {
        Debug.Log("Tower Stat\n" + "Hp: " + hp + " Level: " + level + " Range: " + range + " Ability: " + ability + " Type: " + towerType);
    }
}

public enum TowerType
{
    Scout,
    Shotgun,
    Ranger,
    Minigun,
    Flamethrower,
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
    private TowerStat towerStat;
    private TowerState towerState;

    public void Initialize(TowerStat towerStat)
    {
        this.towerStat = towerStat;
        towerState = TowerState.Disabled;
    }

    public TowerStat GetStat()
    {
        return towerStat;
    }

    public TowerState GetState()
    {
      return towerState;
    }

    public virtual void Action()
    {
        // 추상 메서드
    }

    public void UpdateState(TowerState towerState)
    {
        this.towerState = towerState;
    }

    public TowerStat UpgradeTower(float upgradeMultiplier)
    {
        towerStat = towerStat.Upgrade(upgradeMultiplier);
        return towerStat;
    }
}

public static class TowerFactory
{
    public static Tower CreateTower(Tower towerObject, TowerType towerType)
    {
        TowerStat towerStat = CreateTowerStat(towerType);
        towerObject.Initialize(towerStat);
        return towerObject;
    }

    private static TowerStat CreateTowerStat(TowerType towerType)
    {
        switch (towerType)
        {
            case TowerType.Scout:
                return new TowerStat(10, 1, 1, 1, towerType);
            case TowerType.Shotgun:
                return new TowerStat(10, 1, 1, 2, towerType);
            case TowerType.Ranger:
                return new TowerStat(10, 1, 2, 1, towerType);
            case TowerType.Minigun:
                return new TowerStat(20, 1, 2, 2, towerType);
            case TowerType.Flamethrower:
                return new TowerStat(20, 1, 1, 3, towerType);
            case TowerType.Farm:
                return new TowerStat(10, 1, 0, 1, towerType);
            case TowerType.Commander:
                return new TowerStat(10, 1, 3, 1, towerType);
            case TowerType.Freezer:
                return new TowerStat(10, 1, 3, 2, towerType);
            default:
                return new TowerStat(); // 기본값으로 초기화
        }
    }
}