public interface IEnemy
{
    EnemyStat GetStat();
    EnemyState GetState();
    void SetTarget();
    void PathFind();
    void GetAttack(float damage);
    void Action();
    void UpdateState();
}