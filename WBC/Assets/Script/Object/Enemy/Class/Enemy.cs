using System.Data.SqlTypes;
using UnityEngine;

public struct EnemyStat
{
    public int level;
    public float hp;
    public float range;
    public float damage;
    public int money;
    public float fireRate;
    public EnemyType enemyType;

    public EnemyStat(int level, float hp, float range, float damage, int money, float fireRate, EnemyType enemyType)
    {
        this.level = level;
        this.hp = (level*0.5f) * hp;
        this.range = range;
        this.damage = (level*0.5f) * damage;
        this.money = money;
        this.fireRate = fireRate;
        this.enemyType = enemyType;
    }

    public void ShowStat()
    {
        Debug.Log("Enemy Stat\n" + "Level: " + level + " HP: " + hp + " Range: " + range + " Damage: " + damage + "FireRate:" + fireRate);
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

    public static EnemyStat CreateEnemyStat(EnemyType enemyType, int level)
    {
        switch (enemyType)
        {
            case EnemyType.Normal:
                return new EnemyStat(level, 10, 10, 1, 50, 1, enemyType);
            case EnemyType.Boss:
                return new EnemyStat(level, 50, 15, 5, 300, 1, enemyType);
            default:
                return new EnemyStat(); // 기본값으로 초기화
        }
    }
}
