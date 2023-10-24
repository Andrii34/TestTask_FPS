using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 4.0f;
    [SerializeField] private GameObject spawnArea;
    [SerializeField] private static int maxEnemies = 5;
    [SerializeField] private Transform plaer;
    [SerializeField] private float minDistanceToPlayer = 2f;
    [SerializeField] private float timeLimit = 5.0f;
    [SerializeField] private float minAngle = 0f;
    [SerializeField] private float maxAngle = 360f;

    private int currentEnemies;
    private void Start()
    {
        currentEnemies = 0;
        
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnRateIncrease());
        
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (currentEnemies < maxEnemies)
            {
                float randomAngle = Random.Range(minAngle, maxAngle);
                Quaternion randomRotation = Quaternion.Euler(0f, randomAngle, 0f);
               
                Vector3 spawnPosition = GetRandomSpawnPosition();
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, randomRotation) as GameObject;
                
                // Увеличиваем счетчик врагов
                currentEnemies++;


            }
            yield return new WaitForSeconds(spawnInterval);


        }
    }
    private Vector3 GetRandomSpawnPosition()
    {
        
        Bounds spawnBounds = spawnArea.GetComponent<Renderer>().bounds;
        Vector3 randomPosition = plaer.position;
        while (Vector3.Distance(plaer.position, randomPosition) < minDistanceToPlayer)
        {
            
            randomPosition = new Vector3(

               Random.Range(spawnBounds.min.x, spawnBounds.max.x),
               0.365f,
               Random.Range(spawnBounds.min.z, spawnBounds.max.z)
           );


        }
        return randomPosition;
    }
    private IEnumerator SpawnRateIncrease()
    {
        while (spawnInterval > 1f)
        {
            yield return new WaitForSeconds(timeLimit);


            spawnInterval -= 0.5f;

        }
    }
}
