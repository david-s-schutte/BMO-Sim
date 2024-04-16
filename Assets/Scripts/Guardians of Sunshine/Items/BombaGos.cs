using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaGos : MonoBehaviour
{
    [SerializeField] float throwSpeed = 4;
    [SerializeField] float lifeTime = 5;
    [SerializeField] int attackDamage = 5;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] GameObject explosionObj;

    Rigidbody2D rb;
    AudioSource source;
    [SerializeField] AudioClip explosion;
    CircleCollider2D circleCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ThrowBomba();
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Instantiate(explosion);
            //GetComponent<SpriteRenderer>().enabled = false;
            //circleCollider.enabled = false;
            //source.Stop();
            //source.clip = explosion;
            //source.Play();
            Invoke("RemoveBomba", 2f);

        }
    }

    public void ThrowBomba()
    {
        rb.gravityScale = 0.2f;
        //transform.position = transform.localPosition + new Vector3(0, 1, 0);
        rb.AddForce(new Vector2(transform.forward.z, 0.1f) * throwSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyGoS>().TakeDamage(attackDamage);
            Instantiate(explosion);
            //GetComponent<SpriteRenderer>().enabled = false;
            //circleCollider.enabled = false;
            //source.Stop();
            //source.clip = explosion;
            //source.Play();
            Invoke("RemoveBomba", 2f);
        }
    }

    void RemoveBomba()
    {
        Destroy(this.gameObject);
    }
}
