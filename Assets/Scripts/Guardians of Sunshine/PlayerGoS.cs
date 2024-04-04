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
    [SerializeField] bool hasBomba = true;
    private GameObject thrownBomba;
    [SerializeField] GameObject bomba;

    [Header("Control Settings")]
    [SerializeField] string horizontalAxis = "Horizontal";
    [SerializeField] string verticalAxis = "Vertical";
    [SerializeField] string jumpButton = "Jump1";
    [SerializeField] string attackButton;

    //Components
    Rigidbody2D rb;
    Animator animator;

    //Movement
    Vector2 intendedMovement;
    Vector2 intendedJump;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void GetPlayerInput()
    {
        //Move the player if we aren't attacking
        //if(!Input.GetButton(attackButton) || Input.GetAxis(verticalAxis) >= 0)
            intendedMovement = new Vector2(Input.GetAxis(horizontalAxis) * moveSpeed, 0);
        
        //Make the player jump if they are touching the ground
        if (isGrounded() && Input.GetButton(jumpButton))
            intendedJump = new Vector2(0, jumpHeight);
        else
            intendedJump = Vector2.zero;

        //Engage in combat if the player wishes to
        Attack();

        //Rotate the player depending on last direction given
        if (Input.GetAxis(horizontalAxis) > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        else if (Input.GetAxis(horizontalAxis) < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    void Attack()
    {
        if (Mathf.Abs(intendedMovement.x) > 0)
            return;
        //Can only attacked if grounded and not moving
        if (isGrounded())
        {
            //Use Bomba if special input provided
            if (Input.GetAxis(verticalAxis) < 0  && hasBomba && Input.GetButton(attackButton))
            {
                intendedMovement = Vector2.zero;
                thrownBomba = Instantiate(bomba, transform.position + Vector3.up * 1.1f, Quaternion.identity);
                hasBomba = false;
            }
            else if (Input.GetAxis(verticalAxis) < 0 && Input.GetButton(attackButton))
            {
                intendedMovement = Vector2.zero;
            }
            else if (Input.GetAxis(verticalAxis) < 0 && Input.GetButtonUp(attackButton))
            {
                if(thrownBomba != null)
                    thrownBomba.GetComponent<BombaGos>().ThrowBomba();
            }
            //Regular Attack
            else if(Input.GetButtonDown(attackButton))
            {
                Debug.Log("Attack!");
                //Create a hitbox whenever we press attack and deal damage to the first thing caught in it
                RaycastHit2D hit = Physics2D.BoxCast(transform.position, hitBoxSize, 0, transform.right, hitCheckDistance, enemyLayer);
                if (hit.collider != null)
                    if (hit.collider.GetComponent<EnemyGoS>())
                        hit.collider.GetComponent<EnemyGoS>().TakeDamage(attackDamage);
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

    void UpdateAnimations()
    {
        animator.SetFloat("moveSpeed", Mathf.Abs(intendedMovement.x));
        animator.SetBool("jump", isGrounded());
        animator.SetBool("hasBomba", hasBomba);
        animator.SetBool("throwBomba", Input.GetButton(attackButton));
        animator.SetFloat("crouching", Input.GetAxis(verticalAxis));
        animator.SetBool("attack1", Input.GetButtonDown(attackButton));
        if (animator.GetBool("attack1") && Input.GetButton(attackButton))
            animator.SetBool("attack2", true);
        else
            animator.SetBool("attack2", false);
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
