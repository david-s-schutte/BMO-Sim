using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathGoS : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3;
    private Vector3 source;
    [SerializeField] Transform destination;
    bool digested = false;
    bool passed = false;
    SpriteRenderer sprite;
    Rigidbody2D rb;
    CapsuleCollider2D col;
    [SerializeField] GameObject deadPlayer;
    float halfMark;
    
    // Start is called before the first frame update
    void Start()
    {
        source = transform.position;
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        halfMark = Vector3.Distance(transform.position, destination.position) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(!digested)
        {
            rb.bodyType = RigidbodyType2D.Static;
            col.enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, destination.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, destination.position) < halfMark)
                sprite.enabled = false;
            if (Vector3.Distance(transform.position, destination.position) < 0.1f)
            {
                transform.position = destination.position;
                digested = true;
            }
        }
        else
        {
            if (!passed)
            {
                sprite.enabled = false;
                Instantiate(deadPlayer, this.transform);
                passed = true;
            }
        }
    }
}
