using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 발사체 인터페이스
public interface IProjectile
{   
    void Initialize(float damage);
    float GetDamage();
    void Action();
    void SetTarget(Enemy enemy);
    void HitTarget();
}