using System.Data.SqlTypes;
using UnityEngine;

public struct EnemyStat
{
    public int level;
    public float hp;
    public float range;
    public float damage;
    public int money;
    public EnemyType enemyType;

    public EnemyStat(int level, float hp, float range, float damage, int money, EnemyType enemyType)
    {
        this.hp = (level*0.5f) * hp;
        this.level = level;
        this.range = range;
        this.damage = (level*0.5f) * damage;
        this.money = money;
        this.enemyType = enemyType;
    }

    public void ShowStat()
    {
        Debug.Log("Enemy Stat\n" + "Hp: " + hp + " Level: " + level + " Damage: " + damage + " Type: " + enemyType);
    }
}

public enum EnemyType
{
    Normal,
    Boss
}

public enum EnemyState
{
    Alive,
    Die
}

public class Enemy : MonoBehaviour, IEnemy
{
    protected EnemyStat enemyStat;
    protected EnemyState enemyState;

    public void Initialize(EnemyStat enemyStat)
    {
        this.enemyStat = enemyStat;
        enemyState = EnemyState.Alive;
    }

    public EnemyStat GetStat()
    {
        return enemyStat;
    }

    public EnemyState GetState()
    {
        return enemyState;
    }

    public void GetAttack(float damage)
    {
        enemyStat.hp -= damage;

        enemyStat.ShowStat();
    }

    public virtual void Action()
    {
        // 가상 함수
    }
}


public static class EnemyFactory
{
    public static Enemy CreateEnemy(Enemy enemyObject, EnemyType enemyType, int level = 1)
    {
        EnemyStat enemyStat = CreateEnemyStat(enemyType, level);
        enemyObject.Initialize(enemyStat);
        return enemyObject;
    }

    private static EnemyStat CreateEnemyStat(EnemyType enemyType, int level)
    {
        switch (enemyType)
        {
            case EnemyType.Normal:
                return new EnemyStat(level, 10, 5, 1, 50, enemyType);
            case EnemyType.Boss:
                return new EnemyStat(level, 50, 7.5f, 5, 300, enemyType);
            default:
                return new EnemyStat(); // 기본값으로 초기화
        }
    }
}
