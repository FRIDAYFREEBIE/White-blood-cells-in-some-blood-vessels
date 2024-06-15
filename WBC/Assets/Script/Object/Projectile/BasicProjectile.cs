using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class BasicProjectile : Projectile
{
    private void Update()
    {
        Debug.Log(damage);

        if(target != null)
        {
            Action();
        }
        
        if(target.GetState() == EnemyState.Die)
        {
            GameObject.Destroy(this);
        }
    }

    public override void Action()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 5f * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
        {
            HitTarget();
        }
    }
}
