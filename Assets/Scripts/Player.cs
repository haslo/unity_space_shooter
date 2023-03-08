using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 6f;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleLaserPrefab;
    [SerializeField]
    private Transform _shotsContainer;

    [SerializeField]
    private float _nextFire = 0.0f;
    [SerializeField]
    private float _fireRate = 0.2f;

    [SerializeField]
    private int _lives;

    [SerializeField]
    private bool _poweredUpShot;

    private SpawnManager _spawnManager;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _lives = 3;
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _poweredUpShot = true;
    }

    void Update()
    {
        HandleMovement();
        FirePrimaryWeapon();
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0.0f), 0);
        if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
        else if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
    }

    void FirePrimaryWeapon()
    {
        bool canShoot = Time.time >= _nextFire;
        if (Input.GetKey(KeyCode.Space) && canShoot)
        {
            if (_poweredUpShot)
            {
                GameObject newLaser = Instantiate(_tripleLaserPrefab, transform.position, Quaternion.identity);
                newLaser.transform.parent = _shotsContainer;
            }
            else
            {
                GameObject newLaser = Instantiate(_laserPrefab, transform.position + (Vector3.up * 0.95f), Quaternion.identity);
                newLaser.transform.parent = _shotsContainer;
            }
            _nextFire = Time.time + _fireRate;
        }
    }

    public void Damage()
    {
        _lives -= 1;
        if (_lives < 1)
        {
            Explode();
        }
    }

    public void Explode()
    {
        _spawnManager.StopSpawning();
        Destroy(this.gameObject);
    }
}
