using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlayerGoS : MonoBehaviour
{
    [SerializeField] float timeDelay = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Enemy" || collision.gameObject.tag != "Player")
        {
            Transform originalPlayer = transform.parent;
            if (originalPlayer != null )
            {
                Invoke("EndGame", timeDelay);
            }
            else
            {
                Debug.Log("No Parent!");
            }
        }
    }

    void EndGame()
    {
        Transform originalPlayer = transform.parent;
        originalPlayer.GetComponent<PlayerGoS>().KillPlayer();
    }
}
