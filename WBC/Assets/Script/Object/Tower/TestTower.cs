using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTower : Tower
{
    [Header("towerType")]
    [SerializeField] private TowerType towerType;

    private Tower tower;

    void Start()
    {
        tower = TowerFactory.CreateTower(this, towerType);
        towerStat.ShowStat();
    }

    void Update()
    {
        Action();
    }

    public override void Action()
    {
        float range = towerStat.range;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range);

        foreach (var hitCollider in hitColliders)
        {
            Enemy enemy = hitCollider.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("Enemy detected: " + enemy.name);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (tower != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, towerStat.range);
        }
    }
}
