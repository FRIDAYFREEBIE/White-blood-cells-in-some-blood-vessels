using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class BasicProjectile : Projectile
{
    [Header("Projectile")]    
    [SerializeField] private Projectile projectile;

    void Start()
    {
        Invoke("Die", 1f);
    }

    private void Update()
    {
        if(target != null)
            projectile.Action();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
