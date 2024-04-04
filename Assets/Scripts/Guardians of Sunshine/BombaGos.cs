using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaGos : MonoBehaviour
{
    [SerializeField] bool thrown = false;
    [SerializeField] Vector2 trajectory = new Vector2(1, 0.1f);
    [SerializeField] float throwSpeed = 4;
    [SerializeField] float lifeTime = 5;
    [SerializeField] int attackDamage = 5;
    [SerializeField] LayerMask enemyLayer;

    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (thrown)
        {
            lifeTime -= Time.deltaTime;
            if(lifeTime < 0 )
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void ThrowBomba()
    {
        thrown = true;
        rb.gravityScale = 0.2f;
        rb.AddForce(trajectory * throwSpeed, ForceMode2D.Impulse);
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
