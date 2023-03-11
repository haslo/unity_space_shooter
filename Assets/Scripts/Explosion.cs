using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private static GameObject _explosionAudio;

    void Start()
    {
        Destroy(this.gameObject, 2.8f);
        PlaySound();
    }

    public static void PlaySound()
    {
        if (_explosionAudio == null)
        {
            _explosionAudio = GameObject.Find("/AudioManager/Explosion");
        }
        _explosionAudio.GetComponent<AudioSource>().Play();
    }
}
