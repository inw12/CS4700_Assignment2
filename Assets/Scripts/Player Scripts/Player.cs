using System.Collections;
using UnityEngine;

public enum PlayerState
{
    idle,
    walk,
    attack,
    interact,
    stagger
}

public class Player : MonoBehaviour
{
    public PlayerState currentState;
    public FloatValue speed;
    private Rigidbody2D rigidBody;
    private Vector3 change;
    private Animator animator;
    public FloatValue maxHealth;
    public Signal playerHealthSignal;
    public VectorValue initialPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Application.targetFrameRate = 60;
        currentState = PlayerState.idle;
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        transform.position = initialPosition.value;
    }

    // Update is called once per frame
    void Update()
    {
        // Player MOVEMENT
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");  // 'GetAxisRaw' avoids acceleration values
        change.y = Input.GetAxisRaw("Vertical");
        
        // ATTACK Inputs 
        if (Input.GetButtonDown("Attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger) {
            StartCoroutine(AttackCo());
        } 
        // Movement Inputs
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle) {
            UpdateMovement();   
        }
    }

    void UpdateMovement()   // Method for updating player movement and animations
    {
        if (change != Vector3.zero) // "if a button is pressed..."
        {
            // Character movement
            MoveCharacter();
            // Character looking direction
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            // Character movement status
            animator.SetBool("isMoving", true);
        }
        else    // "if nothing is happening..."
        {
            animator.SetBool("isMoving", false);
        }
    }

    // > MOVEMENT
    void MoveCharacter()
    {
        change.Normalize(); // keeps diagonal movement at same speed
        transform.Translate(speed.value * Time.deltaTime * change);

        /*
        rigidBody.MovePosition(
            transform.position + speed.value * Time.deltaTime * change    // applies transformation to player character
        );
        */
    }

    // Enemy knockback onto the player
    public void Knock(float knockTime, float damage)
    {
        maxHealth.runtimeValue -= damage;
        playerHealthSignal.Raise();
        if (maxHealth.runtimeValue > 0) {
            StartCoroutine(KnockCo(knockTime));
        }
        else {
            this.gameObject.SetActive(false);
        }

    }

    // > ATTACK
    private IEnumerator AttackCo()
    {
        animator.SetBool("isAttacking", true);
        yield return null;
        currentState = PlayerState.attack;

        animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.idle;
    }

    // > KNOCKBACK 
    private IEnumerator KnockCo(float knockTime)
    {
        if (rigidBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            rigidBody.linearVelocity = Vector2.zero;
            currentState = PlayerState.idle;
        }
    }
}
