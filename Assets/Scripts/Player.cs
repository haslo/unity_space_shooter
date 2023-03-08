using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleLaserPrefab;
    [SerializeField]
    private Transform _shotsContainer;

    private float _nextFire = 0.0f;
    private float _fireRate = 0.2f;

    [SerializeField]
    private int _lives;
    private float _speed = 6.0f;
    private bool _tripleShot, _shielded, _speedy;

    private SpawnManager _spawnManager;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _lives = 3;
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _tripleShot = false;
        _shielded = false;
        _speedy = false;
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
        transform.Translate(direction * _speed * (_speedy ? 2.0f : 1.0f) * Time.deltaTime);

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
            if (_tripleShot)
            {
                GameObject newLaser = Instantiate(_tripleLaserPrefab, transform.position, Quaternion.identity);
                newLaser.transform.parent = _shotsContainer;
            }
            else
            {
                GameObject newLaser = Instantiate(_laserPrefab, transform.position + (Vector3.up * 0.95f), Quaternion.identity);
                newLaser.transform.parent = _shotsContainer;
            }
            _nextFire = Time.time + _fireRate / (_speedy ? 2.0f : 1.5f);
        }
    }

    public void SpeedPowerup()
    {
        _speedy = true;
        StartCoroutine(SpeedPowerdown());
    }

    IEnumerator SpeedPowerdown()
    {
        yield return new WaitForSeconds(5.0f);
        _speedy = false;
    }

    public void TripleShotPowerup()
    {
        _tripleShot = true;
        StartCoroutine(TripleShotPowerdown());
    }

    IEnumerator TripleShotPowerdown()
    {
        yield return new WaitForSeconds(5.0f);
        _tripleShot = false;
    }

    public void ShieldPowerup()
    {
        _shielded = true;
        StartCoroutine(ShieldPowerdown());
    }

    IEnumerator ShieldPowerdown()
    {
        yield return new WaitForSeconds(5.0f);
        _shielded = false;
    }

    public void Damage()
    {
        if (!_shielded)
        {
            _lives -= 1;
            if (_lives < 1)
            {
                Explode();
            }
        }
    }

    public void Explode()
    {
        _spawnManager.StopSpawning();
        Destroy(this.gameObject);
    }
}
