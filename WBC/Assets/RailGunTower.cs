using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGunTower : Tower, ITowerStatObserver
{
    [Header("Laser Settings")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float laserWidth; // 레이저의 폭
    [SerializeField] private Color laserColor; // 레이저의 색상
    [SerializeField] private GameObject spawnPoint;

    Enemy closestEnemy;

    void Start()
    {
        lineRenderer.startWidth = laserWidth;
        lineRenderer.endWidth = laserWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // 기본 Shader 사용
        lineRenderer.startColor = laserColor;
        lineRenderer.endColor = laserColor;
    }

    public override void Action()
    {
        closestEnemy = FindClosestEnemy();
        transform.position = new Vector3(transform.position.x, transform.position.y, 1);

        if (closestEnemy != null)
        {
            LookAtEnemy(closestEnemy);

            ShootLaser(closestEnemy);
        }
        else
        {
            lineRenderer.enabled = false;
        }

        BasicEnemy basicEnemy = closestEnemy.GetComponent<BasicEnemy>();

        basicEnemy.Die();
    }

    private Enemy FindClosestEnemy()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 100f);
        Enemy closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = hitCollider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                    if (distanceToEnemy < closestDistance)
                    {
                        closestDistance = distanceToEnemy;
                        closestEnemy = enemy;
                    }
                }
            }
        }

        return closestEnemy;
    }

    private void LookAtEnemy(Enemy enemy)
    {
        Vector2 direction = (enemy.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void ShootLaser(Enemy enemy)
    {
        lineRenderer.enabled = true;

        lineRenderer.SetPosition(0, spawnPoint.transform.position); // 시작점: 타워 위치
        lineRenderer.SetPosition(1, enemy.transform.position); // 끝점: 적 위치
    }

    public void OnTowerStatChanged(TowerStat newStat)
    {
        towerStat = newStat;
    }
}
