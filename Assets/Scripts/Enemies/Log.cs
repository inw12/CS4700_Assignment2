using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Log : Enemy
{
    private Rigidbody2D rigidBody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    // public Transform homePosition;
    private Animator anim;

    void Start()
    {
        currentState = EnemyState.idle;
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate() {
        CheckDistance();
    }

    void CheckDistance()
    {
        // "if distance between log & target is between log's attack and chase radii..."
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius &&
            Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle ||
                currentState == EnemyState.walk &&
                currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                changeAnimation(temp - transform.position);
                rigidBody.MovePosition(temp);
                changeState(EnemyState.walk);
                anim.SetBool("isAwake", true);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("isAwake", false);
        }
    }

    private void changeAnimation(Vector2 direction)
    {
        direction = direction.normalized;
        anim.SetFloat("moveX", direction.x);
        anim.SetFloat("moveY", direction.y);
    }

    private void changeState(EnemyState newState)
    {
        if (currentState != newState) {
            currentState = newState;
        }
    }
}
