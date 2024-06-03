// Tower 인터페이스
public interface ITower
{
    TowerStat GetStat();
    void Action();
    void UpdateState();
    TowerStat UpgradeTower(float upgrademultiplier);
}