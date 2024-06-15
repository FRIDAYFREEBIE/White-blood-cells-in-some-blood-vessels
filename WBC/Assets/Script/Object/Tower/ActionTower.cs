using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BasicTower))]
public class ActionTower : Tower, ITowerStatObserver
{
    [Header("Projectile")]
    [SerializeField] private Projectile projectilePrefab;  // projectile 프리팹을 참조하도록 변경

    private BasicTower basicTower;
    private float fireCooldown;
    private float fireTimer;

    private void Start()
    {
        basicTower = GetComponent<BasicTower>();
        towerStat = basicTower.tower.GetStat();

        basicTower.RegisterObserver(this);

        // 초기 스탯을 기반으로 fireCooldown 설정
        UpdateFireCooldown();
    }

    private void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireCooldown)
        {
            Action();
            fireTimer = 0f;
        }
    }

    public override void Action()
    {
        float range = towerStat.range;

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
            newProjectile.Initialize(towerStat.ability);
            newProjectile.SetTarget(closestEnemy);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (basicTower != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, towerStat.range);
        }
    }

    public void OnTowerStatChanged(TowerStat newStat)
    {
        towerStat = newStat;

        UpdateFireCooldown();

        Debug.Log("ActionTower: 타워의 스탯이 변경되었습니다: " + newStat);
    }

    private void UpdateFireCooldown()
    {
        fireCooldown = 1f / towerStat.fireRate;
    }
}
