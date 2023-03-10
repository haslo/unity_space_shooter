using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab, _tripleLaserPrefab;
    [SerializeField]
    private Transform _shotsContainer;
    [SerializeField]
    private Transform _shieldVisualizer;
    [SerializeField]
    private GameObject _uiManagerObject;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject[] _engines;
    [SerializeField]
    private GameObject _explosionPrefab;

    private float _nextFire = 0.0f;
    private float _fireRate = 0.2f;

    [SerializeField]
    private int _lives;
    private float _speed = 6.0f;
    private bool _tripleShot, _shielded, _speedy;
    private int _score;
    private bool _isDestroyed;

    [SerializeField]
    private GameObject _laserAudio;

    private SpawnManager _spawnManager;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _isDestroyed = false;
        _lives = 3;
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _tripleShot = false;
        _shielded = false;
        _speedy = false;
        _shieldVisualizer.gameObject.SetActive(false);
        _score = 0;
        _uiManager = _uiManagerObject.GetComponent<UIManager>();
        _uiManager.UpdateScore(_score);
        StartCoroutine(EngineFires());
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

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 3.8f), 0);
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
        bool canShoot = Time.time >= _nextFire && !_isDestroyed;
        if (Input.GetKey(KeyCode.Space) && canShoot)
        {
            if (_tripleShot)
            {
                GameObject newLaser = Instantiate(_tripleLaserPrefab, transform.position, Quaternion.identity);
                newLaser.transform.parent = _shotsContainer;
                _laserAudio.GetComponent<AudioSource>().Play();
            }
            else
            {
                GameObject newLaser = Instantiate(_laserPrefab, transform.position + (Vector3.up * 0.95f), Quaternion.identity);
                newLaser.transform.parent = _shotsContainer;
                _laserAudio.GetComponent<AudioSource>().Play();
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
        _shieldVisualizer.gameObject.SetActive(true);
        StartCoroutine(ShieldPowerdown());
    }

    IEnumerator ShieldPowerdown()
    {
        yield return new WaitForSeconds(5.0f);
        shieldsOff();
    }

    private void shieldsOff()
    {
        _shielded = false;
        _shieldVisualizer.gameObject.SetActive(false);
    }

    public void Damage()
    {
        if (_shielded)
        {
            shieldsOff();
        }
        else
        {
            _lives -= 1;
            _uiManager.UpdateLives(_lives);
            if (_lives < 1)
            {
                StartCoroutine(Explode());
            }
        }
    }

    IEnumerator EngineFires()
    {
        while(true)
        {
            foreach (GameObject engine in _engines)
            {
                engine.SetActive(false);
            }
            if (!_isDestroyed)
            {
                for (int i = 0; i < 3 - _lives; i++)
                {
                    bool activatedFire = false;
                    while (!activatedFire)
                    {
                        GameObject engine = _engines[(int)Random.Range(0, 3)];
                        if (!engine.activeSelf)
                        {
                            activatedFire = true;
                            engine.SetActive(true);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(Random.Range(0.1f, 5.0f));
        }
    }

    IEnumerator Explode()
    {
        _isDestroyed = true;
        _spawnManager.StopSpawning();
        GameObject.Find("Thruster").SetActive(false);
        Explosion.PlaySound();
        GameObject explosionAnim = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        explosionAnim.transform.SetParent(this.transform);
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    public void AddToScore(int addToScore)
    {
        _score += addToScore;
        _uiManager.UpdateScore(_score);
    }
}
