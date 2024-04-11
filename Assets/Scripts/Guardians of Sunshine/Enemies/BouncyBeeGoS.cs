using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBeeGoS : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3;
    private Vector3 source, destination;
    [SerializeField] List<Vector3> travelPoints;
    private int currentPoint;
    private bool canMove;
    [SerializeField] float coolDownTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        source = transform.position;
        foreach (Transform child in transform)
            travelPoints.Add(child.position);
        currentPoint = 0;
        destination = travelPoints[currentPoint];
        canMove = true;
        //destination = transform.Find("Destination").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
            return;

        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, destination) < 0.1f)
        {
            transform.position = destination;
            if (currentPoint < travelPoints.Count - 1)
                currentPoint++;
            else
                currentPoint = 0;
            
            destination = travelPoints[currentPoint];
            canMove = false;
            Invoke("CoolDown", coolDownTime);   
        }
    }

    void CoolDown()
    {
        canMove = true;
    }
}
