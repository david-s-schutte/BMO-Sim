using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGoS : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void CollectCoin()
    {
        animator.SetBool("Collected", true);
        Invoke("RemoveCoin", 0.5f);
    }

    void RemoveCoin()
    {
        Destroy(this.gameObject);
    }
}
