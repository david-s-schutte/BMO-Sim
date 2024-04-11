using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGoS : MonoBehaviour
{
    Animator animator;
    AudioSource sfx;

    private void Start()
    {
        animator = GetComponent<Animator>();
        sfx = GetComponent<AudioSource>();
    }

    public void CollectCoin()
    {
        animator.SetBool("Collected", true);
        sfx.Play();
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().AddScore(100);
        Invoke("RemoveCoin", 0.5f);
    }

    void RemoveCoin()
    {
        Destroy(this.gameObject);
    }
}
