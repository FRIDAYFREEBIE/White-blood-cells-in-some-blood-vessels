using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ActionTower))]
public class BasicTower : Tower
{
    [Header("towerType")]
    [SerializeField] private TowerType towerType;

    [HideInInspector]public Tower tower;
    
    private void Start()
    {
        tower = TowerFactory.CreateTower(this, towerType);
        tower.GetStat().ShowStat();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("EnemyProjectile"))
        {
            Projectile projectile = other.gameObject.GetComponent<Projectile>();

            tower.GetAttack(projectile.GetDamage());
        }
    }
}