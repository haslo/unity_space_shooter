using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private Transform _mobsContainer;
    private bool _canSpawn;

    void Start()
    {
        _canSpawn = true;
        StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        _canSpawn = false;
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (_canSpawn)
            {
                Vector3 newPosition = new Vector3(Random.Range(-8f, 8f), 7.5f, 0);
                GameObject newEnemy = Instantiate(_enemyPrefab, newPosition, Quaternion.identity);
                newEnemy.transform.parent = _mobsContainer;
                yield return new WaitForSeconds(Random.Range(1f, 4f));
            }
        }
    }
}
