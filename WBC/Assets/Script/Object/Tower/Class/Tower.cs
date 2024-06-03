using UnityEngine;

public struct TowerStat
{
  public float hp;
  public float level;
  public float range;
  public float multiplier;
  public TowerType towerType;

  public TowerStat(float hp, float level, float range, float multiplier, TowerType towerType)
  {
    this.hp = hp;
    this.range = range;
    this.level = level;
    this.multiplier = multiplier;
    this.towerType = towerType;
  }

  public TowerStat Upgrade(float upgradeMultiplier)
  {
    return new TowerStat(hp * upgradeMultiplier, level + 1, range * upgradeMultiplier, multiplier * upgradeMultiplier, towerType);
  }

  public void ShowStat()
  {
    Debug.Log("hp: " + hp + " level: " + level + " multiplier: " + multiplier + " range: " + range);
  }
}

public enum TowerType
{
  Offensive,
  Support
}

public class Tower : MonoBehaviour, ITower
{
  protected TowerStat towerStat;

  public TowerStat SetStat(TowerType towerType)
  {
    switch(towerType)
    {
      case TowerType.Offensive:
        towerStat = new TowerStat(100, 1, 1, 1, towerType);
        break;

      case TowerType.Support:
        towerStat = new TowerStat(50, 1, 2, 1, towerType);
        break;

      default:
        towerStat = new TowerStat(); // 기본값으로 초기화
        break;
    }

    towerStat.ShowStat();

    return towerStat;
  }

  public TowerStat GetStat()
  {
    return towerStat;
  }
  
  public virtual void Action()
  {
    // 추상 매서드
  }

  public TowerStat UpgradeTower(float upgradeMultiplier)
  {
    towerStat = towerStat.Upgrade(upgradeMultiplier);
    return towerStat;
  }
}
