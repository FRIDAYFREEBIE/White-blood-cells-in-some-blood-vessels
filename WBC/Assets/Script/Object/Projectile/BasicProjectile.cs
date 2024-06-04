using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]

public class BasicProjectile : Projectile
{
    private void Update()
    {
        if(target != null)
        {
            Action();
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
