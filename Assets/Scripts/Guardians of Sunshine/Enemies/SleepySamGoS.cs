using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepySamGoS : MonoBehaviour
{
    [SerializeField] float hitCheckDistance;
    [SerializeField] Vector2 hitBoxSize;
    [SerializeField] float hitTime = 1f;
    [SerializeField] float coolDown = 4f;
    bool isHitting = false;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] GameObject tongue;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartAttack", coolDown);
        animator = GetComponent<Animator>();
        animator.SetBool("gotPlayer", false);
    }

    // Update is called once per frame
    void Update()
    {
        tongue.SetActive(isHitting);
        animator.SetBool("isHitting", isHitting);
    }

    void StartAttack()
    {
        isHitting = true;
        Invoke("EndAttack", hitTime);
    }

    void EndAttack()
    {
        isHitting = false;
        Invoke("StartAttack", coolDown);
    }
}
