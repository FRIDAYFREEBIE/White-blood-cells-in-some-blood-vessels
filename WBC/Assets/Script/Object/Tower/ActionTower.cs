using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BasicTower))]
public class ActionTower : Tower
{
    [Header("Projectile")]
    [SerializeField] private Projectile projectilePrefab;  // projectile 프리팹을 참조하도록 변경

    private BasicTower basicTower;
    private Tower tower;
    private float fireCooldown;
    private float fireTimer;

    private void Start()
    {
        basicTower = GetComponent<BasicTower>();
        tower = basicTower.tower;
        fireCooldown = 1f / tower.GetStat().fireRate;  // 연사력에 따른 쿨다운 설정
        fireTimer = 0f;
    }

    private void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireCooldown)
        {
            Action();
            fireTimer = 0f;  // 타이머 초기화
        }
    }

    public override void Action()
    {
        float range = tower.GetStat().range;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range);
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

        if (closestEnemy != null)
        {
            Projectile newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            newProjectile.Initialize(tower.GetStat().ability);
            newProjectile.SetTarget(closestEnemy);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (tower != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, tower.GetStat().range);
        }
    }
}
