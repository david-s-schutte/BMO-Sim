using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoS : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] int totalHealth = 2;
    [SerializeField] GameObject explosion;
    int currentHealth;
    void Start()
    {
        currentHealth = totalHealth;
    }

    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        if (currentHealth < 1)
        {
            Instantiate(explosion, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            GameObject.FindWithTag("GameController").GetComponent<GameManager>().AddScore(totalHealth * 500);
            GameObject.FindWithTag("GameController").GetComponent<GameManager>().RemoveBoss();
            Destroy(this.gameObject);
        }
    }
}
