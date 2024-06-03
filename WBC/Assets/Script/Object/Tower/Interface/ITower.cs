// Tower 인터페이스
public interface ITower
{
    TowerStat SetStat(TowerType towerType);
    TowerStat GetStat();
    void Action();
    TowerStat UpgradeTower(float upgrademultiplier);
}