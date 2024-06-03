using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : Projectile
{
  [Header("Projectile info")]
  [SerializeField] private int projectileDamage;

  void Start()
  {
    damage = projectileDamage;
  }

  public override void Action()
  {

  }
}