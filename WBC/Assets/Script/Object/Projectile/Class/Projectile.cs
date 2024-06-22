using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    public float damage;
    public GameObject target;

    public void Initialize(float damage)
    {
        this.damage = damage;
    }
    
    public float GetDamage()
    {
        return damage;
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
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
