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
    public AudioClip fireHurt;
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
        if (source.isPlaying)
            return;

        if (rb.velocity.x > 0.1 && !alreadyWalking && main.isGrounded())
        {
            SwitchClip(walk);
            alreadyWalking = true;
            Invoke("ContinueWalkSound", walkDelay);
        }
        if (Input.GetButton("Jump1") && main.isGrounded())
        {
            SwitchClip(jump);
        }
        if(Input.GetButtonDown("Fire1") && main.isGrounded() && main.GetAttacking())
        {
            SwitchClip(hit);
        }
        if (main.GetHurtStatus() && !alreadyHurt)
        {
            SwitchClip(hurt);
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

    public void SwitchClip(AudioClip clip)
    {
        source.Stop();
        source.clip = clip;
        source.Play();
    }
}
