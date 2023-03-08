using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 20f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
        if (transform.position.y > 8.0f)
        {
            if (this.transform.parent != null && this.transform.parent.tag != "SpawnContainer")
            {
                Destroy(this.transform.parent.gameObject);
            }
            Destroy(this.gameObject); 
        }
    }
}
