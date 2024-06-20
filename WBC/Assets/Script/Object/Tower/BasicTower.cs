using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActionTower))]
public class BasicTower : Tower
{
    [Header("towerType")]
    [SerializeField] private TowerType towerType;

    [HideInInspector] public Tower tower;

    public TowerStat publicTowerStat;

    private List<ITowerStatObserver> observers = new List<ITowerStatObserver>();

    private void Start()
    {
        tower = TowerFactory.CreateTower(this, towerType);

        towerStat = tower.GetStat();

        towerStat.ShowStat();

        NotifyObservers();
    }

    void Update()
    {
        publicTowerStat = towerStat;
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
    
    public TowerStat ReturnStat()
    {
        return towerStat;
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
