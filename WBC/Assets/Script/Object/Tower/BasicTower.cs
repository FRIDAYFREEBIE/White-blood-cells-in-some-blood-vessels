using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActionTower))]
public class BasicTower : Tower
{
    [Header("towerType")]
    [SerializeField] private TowerType towerType;

    [HideInInspector] public Tower tower;

    private List<ITowerStatObserver> observers = new List<ITowerStatObserver>();

    private void Start()
    {
        tower = TowerFactory.CreateTower(this, towerType);

        towerStat = tower.GetStat();

        towerStat.ShowStat();

        NotifyObservers();
    }

    public void UpdateTowerStat(TowerStat newStat)
    {
        towerStat = newStat;

        NotifyObservers();
    }

    public void RegisterObserver(ITowerStatObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void UnregisterObserver(ITowerStatObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnTowerStatChanged(towerStat);
        }
    }
    
    public int GetLevel()
    {
        return towerStat.level;
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
