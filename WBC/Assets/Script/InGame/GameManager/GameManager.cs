using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  [Header("Stage Info Container")]
  [SerializeField] private StageInfoContainer stageInfoContainer;

  [Header("EnemySpawner")]
  [SerializeField] private EnemySpawner enemySpawner;

  [Header("Generator")]
  [SerializeField] private TerrainGenerator terrainGenerator;
  [SerializeField] private MapGenerator mapGenerator;
  [SerializeField] private GridGenerator gridGenerator;

  
  public void Start()
  {
    terrainGenerator.Generate();
    mapGenerator.Generate();
  }
  public void StartGame()
  {
    gridGenerator.Generate();

    stageInfoContainer.StartGame();

    enemySpawner.Spawn();
  }

  public void StartStage()
  {
    enemySpawner.Spawn();
  }

  public int CurrentStage()
  {
    return stageInfoContainer.Stage;
  }

  public void NextStage()
  {
    stageInfoContainer.Stage++;
    Debug.Log("Stage: " + stageInfoContainer.Stage);

    ChangeMoney(50*stageInfoContainer.Stage);
    
    StartStage();
  }

  public int CurrentMoney()
  {
    return stageInfoContainer.Money;
  }

  public void ChangeMoney(int amount)
  {
    stageInfoContainer.Money += amount;
    Debug.Log("Money: " + stageInfoContainer.Money);
  }
}