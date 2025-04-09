using System;
using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;    // knockback distance
    public float knockTime; // duration of knockback
    public FloatValue damage;    // amount of damage

    void OnTriggerEnter2D(Collider2D other)
    {
        // "if hitting BREAKABLE objects..."
        if (other.gameObject.CompareTag("Breakable") && this.gameObject.CompareTag("Player")) {
            other.GetComponent<Pot>().destroy();
        }

        // "if hitting an ENEMY or PLAYER..."
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D target = other.GetComponent<Rigidbody2D>(); // the thing we're hitting

            if (target != null)
            {
                // Determine + apply knockback distance
                Vector2 difference = target.transform.position - transform.position;
                difference = difference.normalized * thrust;
                target.AddForce(difference, ForceMode2D.Impulse);

                // "If ENEMY is hit..."
                if (other.gameObject.CompareTag("Enemy") && other.isTrigger)
                {
                    target.GetComponent<Enemy>().currentState = EnemyState.stagger; // Change enemy state
                    other.GetComponent<Enemy>().Knock(target, knockTime, damage);   // Apply player's knockback
                }
                // "if PLAYER is hit..."
                if (other.gameObject.CompareTag("Player"))
                {
                    if (other.GetComponent<Player>().currentState != PlayerState.stagger)
                    {
                        target.GetComponent<Player>().currentState = PlayerState.stagger;   // Change player state
                        other.GetComponent<Player>().Knock(knockTime, damage.value); // Apply enemy's knockback 
                    }
                }
            }
        }
    }
}
