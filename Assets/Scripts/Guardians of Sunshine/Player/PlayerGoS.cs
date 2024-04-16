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
    [SerializeField] float hitCoolDown = 0.2f;
    bool isHitting = false;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] bool hasBomba = true;
    private GameObject thrownBomba;
    [SerializeField] GameObject bomba;

    [Header("Player Health")]
    [SerializeField] int lives = 5;
    [SerializeField] Vector2 spawnPoint;
    [SerializeField] bool isHit = false;
    [SerializeField] float hitRecovery = 5f;

    [Header("Control Settings")]
    [SerializeField] string horizontalAxis = "Horizontal";
    [SerializeField] string verticalAxis = "Vertical";
    [SerializeField] string jumpButton = "Jump1";
    [SerializeField] string attackButton;
    [SerializeField] string bombaButton;



    //Components
    Rigidbody2D rb;
    Animator animator;
    PlayerDeathGoS pDeath;
    SpriteRenderer spriteRenderer;

    //Movement
    Vector2 intendedMovement;
    Vector2 intendedJump;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spawnPoint = transform.position;
        pDeath = GetComponent<PlayerDeathGoS>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        //if(!Input.GetButton(bombaButton))
        //if(isGrounded())
        intendedMovement = new Vector2(Input.GetAxis(horizontalAxis) * moveSpeed, 0);

        if (Input.GetButton(bombaButton))
            rb.velocity = new Vector2(0, rb.velocity.y);

        //Make the player jump if they are touching the ground
        if (isGrounded() && Input.GetButton(jumpButton) && !Input.GetButton(bombaButton))
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
        //Can only attacked if grounded and not moving
        if (Mathf.Abs(rb.velocity.x) > 0.1)
            return;

        if (isHitting)
        {
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, hitBoxSize, 0, transform.right, hitCheckDistance, enemyLayer);
            if (hit.collider != null)
                if (hit.collider.GetComponent<EnemyGoS>())
                    hit.collider.GetComponent<EnemyGoS>().TakeDamage(attackDamage);
        }

        if (isGrounded())
        {
            //Bomba Attack
            if (hasBomba && Input.GetButtonDown(bombaButton))
            {
                rb.velocity = Vector2.zero;
                hasBomba = false;
                thrownBomba = Instantiate(bomba, transform.position + Vector3.up * 2f, Quaternion.LookRotation(transform.forward));
            }
            //Regular Attack
            else if(Input.GetButtonDown(attackButton) && !isHitting)
            {
                isHitting = true;
                Invoke("HitCoolDown", hitCoolDown);
            }
        }

    }

    void MovePlayer()
    {
        rb.AddForce(intendedMovement, ForceMode2D.Force);
        rb.AddForce(intendedJump, ForceMode2D.Impulse);
    }

    public bool isGrounded()
    {
        if(Physics2D.BoxCast(transform.position, groundBoxSize, 0, -transform.up, groundCheckDistance, groundLayer))
            return true; 
        return false;
    }

    void UpdateAnimations()
    {
        animator.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("jump", isGrounded());
        animator.SetBool("hasBomba", hasBomba);
        animator.SetBool("throwBomba", Input.GetButton(bombaButton));
        animator.SetFloat("crouching", Input.GetAxis(verticalAxis));
        animator.SetBool("attack1", isHitting);
        if (isHit)
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
        else
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
            collision.gameObject.GetComponent<CoinGoS>().CollectCoin();

        if (collision.tag == "Checkpoint")
            spawnPoint = collision.transform.position;

        if(collision.tag == "Enemy")
        {
            rb.AddForce(new Vector2(-transform.forward.z, 1) * jumpHeight, ForceMode2D.Impulse);
            isHit = true;
            Invoke("DamageCoolDown", hitRecovery);
            lives--;
        }

        if(collision.tag == "Tongue")
        {
            //Debug.Log("eated up");
            collision.transform.parent.GetComponent<SleepySamGoS>().animator.SetBool("gotPlayer", true);
            pDeath.enabled = true;
            this.GetComponent<PlayerGoS>().enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isHit)
            return;

        if(collision.gameObject.tag == "Enemy" && collision.gameObject.layer == 3)
        {
            rb.velocity= Vector3.zero;
            transform.position = spawnPoint;
            lives--;
            GetComponent<PlayerSoundsGoS>().SwitchClip(GetComponent<PlayerSoundsGoS>().fireHurt);
        }
        else if(collision.gameObject.tag == "Enemy")
        {
            rb.AddForce(new Vector2(-transform.forward.z, 1) * jumpHeight, ForceMode2D.Impulse);
            isHit = true;
            Invoke("DamageCoolDown", hitRecovery);
            lives--;
        }
    }

    void HitCoolDown()
    {
        isHitting = false;
    }

    void DamageCoolDown()
    {
        isHit = false;
    }

    public int GetHealth()
    {
        return lives;
    }

    public bool GetHasBomba()
    {
        return hasBomba;
    }

    public bool GetAttacking()
    {
        return isHitting;
    }

    public bool GetHurtStatus()
    {
        return isHit;
    }

    public void KillPlayer()
    {
        lives = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * groundCheckDistance, groundBoxSize);
        Gizmos.DrawWireCube(transform.position + transform.right * hitCheckDistance, hitBoxSize);
    }
}
