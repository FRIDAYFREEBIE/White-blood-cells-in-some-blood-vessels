public interface IEnemy
{
    EnemyStat GetStat();
    EnemyState GetState();
    void GetAttack(float damage);
    void Action();
}