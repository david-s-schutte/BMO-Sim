using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoS : MonoBehaviour
{
    [Header("Physics Settings")]
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float jumpHeight = 10;
    [SerializeField] float groundCheckDistance;
    [SerializeField] Vector2 groundBoxSize;
    [SerializeField] LayerMask groundLayer;

    [Header("Combat Settings")]
    [SerializeField] int attackDamage;
    [SerializeField] float hitCheckDistance;
    [SerializeField] Vector2 hitBoxSize;
    [SerializeField] LayerMask enemyLayer;

    [Header("Control Settings")]
    [SerializeField] string horizontalAxis = "Horizontal";
    [SerializeField] string jumpButton = "Jump1";
    [SerializeField] string attackButton;

    //Components
    Rigidbody2D rb;

    //Movement
    Vector2 intendedMovement;
    Vector2 intendedJump;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
        MovePlayer();
        Attack();
    }

    void GetPlayerInput()
    {
        intendedMovement = new Vector2(Input.GetAxis(horizontalAxis) * moveSpeed, 0);
        if (isGrounded() && Input.GetButtonDown(jumpButton))
            intendedJump = new Vector2(0, jumpHeight);
        else
            intendedJump = Vector2.zero;
    }

    void Attack()
    {
        if (isGrounded() && Input.GetButtonDown(attackButton))
        {
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, hitBoxSize, 0, transform.right, hitCheckDistance, enemyLayer);
            if(hit.collider != null)
            {
                if (hit.collider.GetComponent<EnemyGoS>())
                    hit.collider.GetComponent<EnemyGoS>().TakeDamage(attackDamage);
            }
            else
            {
                Debug.Log("No enemy!");
            }
        }

    }

    void MovePlayer()
    {
        rb.AddForce(intendedMovement, ForceMode2D.Force);
        rb.AddForce(intendedJump, ForceMode2D.Impulse);
    }

    bool isGrounded()
    {
        if(Physics2D.BoxCast(transform.position, groundBoxSize, 0, -transform.up, groundCheckDistance, groundLayer))
            return true; 
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * groundCheckDistance, groundBoxSize);
        Gizmos.DrawWireCube(transform.position + transform.right * hitCheckDistance, hitBoxSize);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
