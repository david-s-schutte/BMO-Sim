using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunnyBunnyGoS : MonoBehaviour
{
    [SerializeField] float waitTime;
    [SerializeField] GameObject honeyGlob;
    [SerializeField] Transform firingPoint;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ShootHoneyGlob", waitTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShootHoneyGlob()
    {
        Instantiate(honeyGlob, firingPoint.position, Quaternion.identity);
        Invoke("ShootHoneyGlob", waitTime);
    }
}
