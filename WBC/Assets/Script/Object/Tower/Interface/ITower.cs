// Tower 인터페이스
public interface ITower
{
    TowerStat GetStat();
    TowerState GetState();
    void Action();
    void GetAttack(float damage);
    void UpdateState(TowerState towerState);
    TowerStat UpgradeTower(float upgrademultiplier);
}