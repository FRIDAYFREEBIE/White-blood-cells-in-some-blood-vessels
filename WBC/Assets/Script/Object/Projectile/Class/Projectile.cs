using UnityEngine;

public struct ProjectileStat
{
    public int damage;

    public ProjectileStat(int damage)
    {
        this.damage = damage;
    }

    public void ShowStat()
    {
        Debug.Log("damage: " + damage);
    }
}

public class Projectile : MonoBehaviour, IProjectile
{
    public ProjectileStat SetStat(int damage)
    {
        ProjectileStat projectileStat = new ProjectileStat(damage);
        return projectileStat;
    }

    public virtual void Action()
    {

    }
}