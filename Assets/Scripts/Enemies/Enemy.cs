using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using System;
using System.Collections;
using System.Runtime.CompilerServices;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth;
    private float currentHealth;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    private void Awake()
    {
        currentHealth = maxHealth.value;
    } 

    private void takeDamage(FloatValue damage)
    {
        currentHealth -= damage.value;
        if (currentHealth <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Knock(Rigidbody2D rigidBody, float knockTime, FloatValue damage)
    {
        StartCoroutine(KnockCo(rigidBody, knockTime));
        takeDamage(damage);
    }

    private IEnumerator KnockCo(Rigidbody2D rigidBody, float knockTime)
    {
        if (rigidBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            rigidBody.linearVelocity = Vector2.zero;
            rigidBody.GetComponent<Enemy>().currentState = EnemyState.idle;
        }
    }
}
