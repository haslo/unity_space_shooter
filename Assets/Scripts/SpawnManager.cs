using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Powerup;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject[] _powerupPrefabs;
    [SerializeField]
    private Transform _mobsContainer, _powerupsContainer;
    private bool _canSpawn;

    void Start()
    {
        _canSpawn = false;
    }

    public void StartSpawning()
    {
        _canSpawn = true;
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    public void StopSpawning()
    {
        _canSpawn = false;
    }

    IEnumerator EnemySpawnRoutine()
    {
        while (_canSpawn)
        {
            Vector3 newPosition = new Vector3(Random.Range(-8f, 8f), 7.5f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, newPosition, Quaternion.identity);
            newEnemy.transform.parent = _mobsContainer;
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        }
    }

    IEnumerator PowerupSpawnRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        while (_canSpawn)
        {
            Vector3 newPosition = new Vector3(Random.Range(-8f, 8f), 7.5f, 0);
            GameObject powerup = Instantiate(_powerupPrefabs[(int)Random.Range(0, _powerupPrefabs.Length)], newPosition, Quaternion.identity);
            powerup.transform.parent = _powerupsContainer;
            yield return new WaitForSeconds(Random.Range(5f, 10f));
        }
    }
}
