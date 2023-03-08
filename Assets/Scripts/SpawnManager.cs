using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _tripleShotPowerupPrefab;
    [SerializeField]
    private Transform _mobsContainer;
    [SerializeField]
    private Transform _powerupsContainer;
    private bool _canSpawn;

    void Start()
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
        while (true)
        {
            if (_canSpawn)
            {
                Vector3 newPosition = new Vector3(Random.Range(-8f, 8f), 7.5f, 0);
                GameObject newEnemy = Instantiate(_enemyPrefab, newPosition, Quaternion.identity);
                newEnemy.transform.parent = _mobsContainer;
            }
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        }
    }

    IEnumerator PowerupSpawnRoutine()
    {
        while (true)
        {
            if (_canSpawn)
            {
                Vector3 newPosition = new Vector3(Random.Range(-8f, 8f), 7.5f, 0);
                GameObject newEnemy = Instantiate(_tripleShotPowerupPrefab, newPosition, Quaternion.identity);
                newEnemy.transform.parent = _powerupsContainer;
            }
            yield return new WaitForSeconds(Random.Range(10f, 15f));
        }
    }
}
