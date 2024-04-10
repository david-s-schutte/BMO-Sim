using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaGos : MonoBehaviour
{
    [SerializeField] float throwSpeed = 4;
    [SerializeField] float lifeTime = 5;
    [SerializeField] int attackDamage = 5;
    [SerializeField] LayerMask enemyLayer;

    Rigidbody2D rb;


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
            Destroy(this.gameObject);
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
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
