using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Serialized Fields for attributes
    [SerializeField]
    float movementSpeed = 1f;

    [SerializeField]
    ControlMode controlMode;

    //Variables used within this script
    Rigidbody2D rigidBody;
    Collider2D coll2D;

    Vector2 playerHalfSize = Vector2.zero;

    Vector2 facingDirection = Vector2.right;

    [SerializeField]
    Sprite[] directionalSprites;

    

    SpriteRenderer sr;

    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {

        rigidBody = GetComponent<Rigidbody2D>();
        if(rigidBody == null)
        {
            Debug.Log("This object is missing a rigidbody!");
        }

        coll2D = GetComponent<Collider2D>();
        if(coll2D != null)
        {
            playerHalfSize = coll2D.bounds.extents;
        }

        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidBody == null)
        {
            return;
        }
        Vector2 dir = Vector2.zero;

        if (controlMode == ControlMode.WASD)
        {
            dir = WASDControl();
           
        }

        if (controlMode == ControlMode.ARROWKEYS)
        {
            dir = ArrowKeysControl();
        }

        rigidBody.linearVelocity = dir.normalized * movementSpeed;

        HandleAnimations(dir != Vector2.zero);

        if(dir != Vector2.zero)
        {
            facingDirection = dir;
        }

    }


    Vector2 WASDControl()
    {
        Vector2 direction = Vector2.zero;

        float verticalBound = Camera.main.orthographicSize - playerHalfSize.y;
        float horizontalBound = Camera.main.orthographicSize * Camera.main.aspect - playerHalfSize.x;

        if (transform.position.y < verticalBound && Keyboard.current.wKey.isPressed)
        {
            direction.y = 1;
            UpdateDirection(0);
        }
        if (transform.position.y > -verticalBound && Keyboard.current.sKey.isPressed)
        {
            direction.y = -1;
            UpdateDirection(2);
        }
        if (transform.position.x < horizontalBound && Keyboard.current.dKey.isPressed)
        {
            direction.x = 1;
            UpdateDirection(1);
        }
        if (transform.position.x > -horizontalBound && Keyboard.current.aKey.isPressed)
        {
            direction.x = -1;
            UpdateDirection(3);
        }

        return direction;
    }

    Vector2 ArrowKeysControl()
    {
        Vector2 direction = Vector2.zero;

        float verticalBound = Camera.main.orthographicSize - playerHalfSize.y;
        float horizontalBound = Camera.main.orthographicSize * Camera.main.aspect - playerHalfSize.x;

        if (transform.position.y < verticalBound && Keyboard.current.upArrowKey.isPressed)
        {
            direction.y = 1;
            UpdateDirection(2);
        }
        if (transform.position.y > -verticalBound && Keyboard.current.downArrowKey.isPressed)
        {
            direction.y = -1;
            UpdateDirection(3);
        }
        if (transform.position.x < horizontalBound && Keyboard.current.rightArrowKey.isPressed)
        {
            direction.x = 1;
            UpdateDirection(0);
        }
        if (transform.position.x > -horizontalBound && Keyboard.current.leftArrowKey.isPressed)
        {
            direction.x = -1;
            UpdateDirection(1);
        }

        return direction;
    }

    public Vector2 GetFacingDirection()
    {
        return facingDirection;
    }

    public void UpdateDirection(int direction)
    {
        // 0 - EAST
        // 1- WEST
        // 2- NORTH
        // 3 - SOUTH

        if(sr == null || directionalSprites.Length <= direction)
        {
            return;
        }


        sr.sprite = directionalSprites[direction];
    }


    void HandleAnimations(bool isMoving)
    {
        if(anim == null)
        {
            return;
        }

        if (isMoving)
        {
            bool canPlay = false;
            if(facingDirection.y > 0 )
            {
                canPlay = TryPlayAnimation("walk_up");
            }else if (facingDirection.x > 0)
            {
                canPlay = TryPlayAnimation("walk_right");
            }else if  (facingDirection.x < 0)
            {
                canPlay = TryPlayAnimation("walk_left");
            }else if (facingDirection.y < 0 )
            {
                canPlay = TryPlayAnimation("walk_down");
            }

            if(canPlay == false)
            {
                TryPlayAnimation("walk");
            }
        }
        else
        {
            bool canPlay = false;
            if (facingDirection.y > 0)
            {
                canPlay = TryPlayAnimation("idle_up");
            }
            else if (facingDirection.x > 0)
            {
                canPlay = TryPlayAnimation("idle_right");
            }
            else if (facingDirection.x < 0)
            {
                canPlay = TryPlayAnimation("idle_left");
            }
            else if (facingDirection.y < 0)
            {
                canPlay = TryPlayAnimation("idle_down");
            }


            if (canPlay == false)
            {
                TryPlayAnimation("idle");
            }
        }
        

    }

    bool TryPlayAnimation(string animationName)
    {
        int animHash = Animator.StringToHash(animationName);

        if (anim.HasState(0, animHash))
        {
            anim.Play(animHash);
            return true;
        }

        return false;
    }
}


public enum ControlMode
{
    WASD,
    ARROWKEYS
}