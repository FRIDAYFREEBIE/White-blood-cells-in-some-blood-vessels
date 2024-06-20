using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    public float damage;
    public Enemy target;

    public void Initialize(float damage)
    {
        this.damage = damage;
    }
    
    public float GetDamage()
    {
        return damage;
    }

    public void SetTarget(Enemy enemy)
    {
        target = enemy;
    }

    public virtual void Action()
    {

    }

    public void HitTarget()
    {
        if (target != null)
        {
            Destroy(gameObject);
        }
    }
}
