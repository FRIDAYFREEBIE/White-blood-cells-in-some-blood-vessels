using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : Projectile
{
  [Header("Projectile info")]
  [SerializeField] private int damage;

  private ProjectileStat projectileStat;

  void Start()
  {
    projectileStat = SetStat(damage);
  }

  public override void Action()
  {

  }
}