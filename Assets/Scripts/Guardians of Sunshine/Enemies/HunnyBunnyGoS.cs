using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunnyBunnyGoS : MonoBehaviour
{
    [SerializeField] float waitTime;
    [SerializeField] GameObject honeyGlob;
    [SerializeField] Transform firingPoint;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ShootHoneyGlob", waitTime);
        animator = GetComponent<Animator>();
    }

    void ShootHoneyGlob()
    {
        animator.SetBool("fire", true);
        Instantiate(honeyGlob, firingPoint.position, Quaternion.identity);
        Invoke("ShootHoneyGlob", waitTime);
        //animator.SetBool("fire", false);
    }
}
