using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundsGoS : MonoBehaviour
{
    AudioSource source;
    [SerializeField] AudioClip walk;
    [SerializeField] float walkDelay = 0.2f;
    bool alreadyWalking = false;
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip hurt;
    [SerializeField] float hurtDelay = 0.2f;
    bool alreadyHurt = false;

    Rigidbody2D rb;
    PlayerGoS main;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        main = GetComponent<PlayerGoS>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(source.isPlaying);

        if (source.isPlaying)
            return;

        if (rb.velocity.x > 0.1 && !alreadyWalking && main.isGrounded())
        {
            alreadyWalking = true;
            source.clip = walk;
            source.Play();
            Invoke("ContinueWalkSound", walkDelay);
        }
        if (Input.GetButton("Jump1") && main.isGrounded())
        {
            source.clip = jump;
            source.Play();
        }
        if(Input.GetButtonDown("Fire1") && main.isGrounded() && main.GetAttacking())
        {
            source.clip = hit;
            source.Play();
        }
        if (main.GetHurtStatus() && !alreadyHurt)
        {
            source.clip = hurt;
            source.Play();
            alreadyHurt = true;
            Invoke("RefreshHurtSound", hurtDelay);
        }
    }

    void ContinueWalkSound()
    {
        alreadyWalking = false;
    }

    void RefreshHurtSound()
    {
        alreadyHurt = false;
    }
}
