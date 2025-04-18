using Unity.VisualScripting;
using UnityEngine;

public class BoundedNPC : Sign
{
    private Vector3 directionVector;
    private Transform myTransform;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Animator anim;
    public Collider2D bounds;

    private bool isMoving;
    public float minMoveTime;
    public float maxMoveTime;
    private float moveTimeSeconds;
    public float minWaitTime;
    public float maxWaitTime;
    private float waitTimeSeconds;

    // START
    void Start()
    {
        Application.targetFrameRate = 60;
        moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
        waitTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
        anim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody2D>();
        ChangeDirection();
    }

    // UPDATE
    public override void Update()
    {
        base.Update();

        if (isMoving)
        {
            moveTimeSeconds -= Time.deltaTime;
            if(moveTimeSeconds <= 0) 
            {
                moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
                isMoving = false;
            }
            // "if Player is not in range..."
            if (!playerInRange)
            {
                Move();
            }
        }
        else
        {
            waitTimeSeconds -= Time.deltaTime;
            if (waitTimeSeconds <= 0)
            {
                ChooseDifferentDirection();
                waitTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
                isMoving = true;
            }
        }         
    }

    // (METHOD) Movement Method
    private void Move()
    {
        Vector3 temp = myTransform.position + speed * Time.deltaTime * directionVector;

        if (bounds.bounds.Contains(temp))
        {
            myRigidbody.MovePosition(temp);
        }
        else
        {
            ChangeDirection();
        }
    } 

    // (METHOD) Update NPC animation depending on direction
    void ChangeDirection()
    {
        int direction = Random.Range(0, 4);
        switch(direction)
        {
            case 0:
                // Right
                directionVector = Vector3.right;
                break;
            case 1:
                // Up
                directionVector = Vector3.up;
                break;
            case 2:
                // Left
                directionVector = Vector3.left;
                break;
            case 3:
                // Down
                directionVector = Vector3.down;
                break;
            default:
                break;
        }
        UpdateAnimation();  // Update animation according to direction
    }

    // (METHOD) Changes NPC's looking direction
    void UpdateAnimation()
    {
        anim.SetFloat("moveX", directionVector.x);
        anim.SetFloat("moveY", directionVector.y);
    }

    private void ChooseDifferentDirection()
    {
        Vector3 temp = directionVector;
        ChangeDirection();
        int loops = 0;
        while (temp == directionVector && loops < 100)
        {
            loops++;
            ChangeDirection();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ChooseDifferentDirection();
    }
}