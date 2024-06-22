using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicEnemy))]
public class ActionEnemy : Enemy
{
    [Header("Enemy")]    
    [SerializeField] private Enemy action;

    private BasicEnemy basicEnemy;
    private float fireCooldown;
    private float fireTimer;

    private void Start()
    {
        basicEnemy = GetComponent<BasicEnemy>();

        enemyStat = basicEnemy.publicEnemyStat;
        
        UpdateFireCooldown();
    }

    private void Update()
    {

        fireTimer += Time.deltaTime;

        if (fireTimer >= fireCooldown)
        {
            action.Action();
            fireTimer = 0f;
        }

        enemyStat = basicEnemy.publicEnemyStat;
    }

    private void UpdateFireCooldown()
    {
        fireCooldown = 1f / enemyStat.fireRate;
    }
}
