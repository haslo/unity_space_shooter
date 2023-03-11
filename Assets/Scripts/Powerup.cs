using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum
        PowerupType
    {
        Speed,
        TripleShot,
        Shield
    }

    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private PowerupType _powerupType;
    [SerializeField]
    private GameObject _powerupAudio;

    void Start()
    {
        _powerupAudio = GameObject.Find("/AudioManager/PowerUp");
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
                switch (_powerupType) {
                    case PowerupType.Shield:
                        player.ShieldPowerup();
                        _powerupAudio.GetComponent<AudioSource>().Play();
                        break;
                    case PowerupType.TripleShot:
                        player.TripleShotPowerup();
                        _powerupAudio.GetComponent<AudioSource>().Play();
                        break;
                    case PowerupType.Speed:
                        player.SpeedPowerup();
                        _powerupAudio.GetComponent<AudioSource>().Play();
                        break;
                    default:
                        break;
                }
                Destroy(this.gameObject);
            }
        }
    }
}
