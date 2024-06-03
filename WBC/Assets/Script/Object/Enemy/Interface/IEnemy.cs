public interface IEnemy
{
    EnemyStat GetStat();
    EnemyState GetState();
    void SetTarget();
    void PathFind();
    void Action();
    void UpdateState();
}