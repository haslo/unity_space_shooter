using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotPowerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    void Start()
    {
        // noop
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
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Powerup();
                Destroy(this.gameObject);
            }
        }
    }
}
