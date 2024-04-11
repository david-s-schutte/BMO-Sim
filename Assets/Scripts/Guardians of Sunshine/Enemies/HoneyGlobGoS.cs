using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyGlobGoS : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float lifeTime = 1;
    [SerializeField] float shootForce = 3;
    [SerializeField] Vector2 shootDir;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(shootDir * shootForce, ForceMode2D.Impulse);
        Invoke("RemoveBullet", lifeTime);
    }

    void RemoveBullet()
    {
        Destroy(this.gameObject);
    }
}
