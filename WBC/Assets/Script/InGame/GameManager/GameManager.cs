using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    stageInfoContainer.StartGame();
    
    ChangeMoney(100000);
  }

  void Update()
  {
    if(stageInfoContainer.Stage != 1)
    {
      if(isAllDead())
      {
        GameOver();
      }
    }
  }

  void GameOver()
  {
    SceneManager.LoadScene("GameOver");
  }

  bool isAllDead()
  {
    GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");

    return towers.Length == 0;
  }

  public void StartGame()
  {
    gridGenerator.Generate();

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
  }
}