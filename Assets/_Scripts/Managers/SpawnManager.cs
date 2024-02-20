using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;
public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private int cloneCount;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private List<GameObject> cloneList = new List<GameObject>();

    public List<GameObject> activeObject = new List<GameObject>();
    
    private Coroutine spawnCoroutine;
    private void OnEnable()
    {
        UIManager.OnGameStart += UIManager_OnGameStart;
        UIManager.OnGameReset += UIManager_OnGameReset;
    }
    private void OnDisable()
    {
        UIManager.OnGameStart -= UIManager_OnGameStart;
        UIManager.OnGameReset -= UIManager_OnGameReset;
    }
    private void Awake()
    {
        InstantiateObstacles();
    }
    private IEnumerator SpawnObstacle() {
        while (true)
        {
            yield return new WaitForSeconds(3f); 
            GetRandomObstacle(spawnPoints[0]);
            GetRandomObstacle(spawnPoints[1]);
            GetRandomObstacle(spawnPoints[2]);
        }
    }
    private void GetRandomObstacle(Transform line) {
        var obstacleClone = cloneList[Random.Range(0, cloneList.Count)];
        var position = line.position;
        obstacleClone.transform.position = new Vector3(position.x, position.y,Player.transform.position.z + 60);
        cloneList.Remove(obstacleClone);
        activeObject.Add(obstacleClone);
        obstacleClone.SetActive(true);
    }
    private void InstantiateObstacles()
    {
        foreach (var obstacle in obstaclePrefabs)
        {
            for (int i = 0; i < cloneCount; i++)
            {
                var obstacleClone = Instantiate(obstacle);
                cloneList.Add(obstacleClone);
                obstacleClone.SetActive(false);
            }
        }
    }
    
    private void UIManager_OnGameStart()
    {
        spawnCoroutine = StartCoroutine(SpawnObstacle());
    }
    
    private void UIManager_OnGameReset()
    {
        StopCoroutine(spawnCoroutine);
        foreach (var obj in activeObject)
        {
            obj.SetActive(false); 
            cloneList.Add(obj);
        }
    }

    
    
}
