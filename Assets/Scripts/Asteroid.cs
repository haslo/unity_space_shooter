using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Asteroid : MonoBehaviour
{
    private float _rotationSpeed;
    [SerializeField]
    private GameObject _explosionPrefab;
    private bool _isDestroyed;

    void Start()
    {
        _rotationSpeed = 40f;
        _isDestroyed = false;
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isDestroyed)
        {
            if (other.tag == "DestructibleShot")
            {
                Destroy(other.gameObject);
                Explode();
            }
            else if (other.tag == "IndestructibleShot")
            {
                Explode();
            }
        }
    }

    public void Explode()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        _isDestroyed = true;
        Destroy(this.gameObject, 0.5f);
    }
}
