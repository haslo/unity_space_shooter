using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    private static Player _player;
    private Animator _animator;
    private bool _isDestroyed;

    void Start()
    {
        if (_player == null)
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
        }
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Animator not present");
        }
        _isDestroyed = false;
    }

    void Update()
    {
        transform.Translate(Vector2.down * _speed * Time.deltaTime);
        if (transform.position.y < -8.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isDestroyed)
        {
            if (other.tag == "Player")
            {
                Player player = other.transform.GetComponent<Player>();
                if (player != null)
                {
                    player.Damage();
                    Damage();
                }
            }
            else if (other.tag == "DestructibleShot")
            {
                Destroy(other.gameObject);
                _player.AddToScore(10);
                Damage();
            }
            else if (other.tag == "IndestructibleShot")
            {
                Damage();
            }
        }
    }

    public void Damage()
    {
        Explode();
    }

    public void Explode()
    {
        _animator.SetTrigger("OnEnemyDeath");
        _isDestroyed = true;
        Explosion.PlaySound();
        Destroy(this.gameObject, 2.8f);
    }
}
