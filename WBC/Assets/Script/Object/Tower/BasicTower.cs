using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActionTower))]
public class BasicTower : Tower
{
    [Header("towerType")]
    [SerializeField] private TowerType towerType;

    [HideInInspector] public Tower tower;
    [HideInInspector] public TowerStat publicTowerStat;
    [HideInInspector] public bool isCommander;

    private List<ITowerStatObserver> observers = new List<ITowerStatObserver>();

    private void Start()
    {
        tower = TowerFactory.CreateTower(this, towerType);

        towerStat = tower.GetStat();

        //towerStat.ShowStat();

        NotifyObservers();
    }

    void Update()
    {
        publicTowerStat = towerStat;

        Commander();

        NotifyObservers();
    }

    public void UpdateTowerStat(TowerStat newStat)
    {
        towerStat = newStat;
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

    public void Commander()
    {
        float temp = towerStat.ability;

        if(isCommander)
            towerStat.ability = temp * 1.1f; 
        else
            towerStat.ability = temp;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("EnemyProjectile"))
        {
            Projectile projectile = other.gameObject.GetComponent<Projectile>();

            if(projectile != null)
            GetAttack(projectile.GetDamage());

            Destroy(other.gameObject);

            towerStat.ShowStat();

            if(towerStat.hp <= 0)
                Die();
        }
    }

    public void Die()
    {
        towerState = TowerState.Disabled;

        Destroy(gameObject);
    }
}
