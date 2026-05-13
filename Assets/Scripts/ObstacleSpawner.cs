using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    //Basic
    [SerializeField] private GameObject m_obstaclePrefab;
    private GameObject spawner;
    
    //Coroutines
    private Coroutine _obstacleSpawner;

    private void Awake()
    {
        spawner =gameObject;
    }

    private void Start()
    {
        spawnObstacle(m_obstaclePrefab);
        _obstacleSpawner = StartCoroutine(SpawnObstacle());
    }

    //Function to spawn obstacles and make them move.
    private void spawnObstacle(GameObject obstacle)
    {
        float speed = 0;
        GameObject newObstacle = Instantiate(obstacle, spawner.transform.position, obstacle.transform.rotation);
        if (newObstacle.TryGetComponent(out ObstacleLogic OL))
        {
            speed = OL.MovementSpeed;
        }
        if (newObstacle.TryGetComponent(out Rigidbody2D rb))
        {
            rb.AddForceX(-speed, ForceMode2D.Impulse);
        }
    }

    //Coroutine to continuously spawn obstacles.
    private IEnumerator SpawnObstacle()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 5));
            spawnObstacle(m_obstaclePrefab);
        }
    }
    
}
