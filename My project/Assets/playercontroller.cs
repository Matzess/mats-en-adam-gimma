using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playercontroller : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    Vector2 movementInput;
    SpriteRenderer SpriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
    if(movementInput != Vector2.zero){

        bool succes = TryMove(movementInput);

        if(!succes) {
            succes = TryMove(new Vector2(movementInput.x, 0));

    
            }

              if(!succes) {
            succes = TryMove(new Vector2(0, movementInput.y));
                }

            animator.SetBool("ismoving", succes);
            }
            else{ 
                animator.SetBool("ismoving", false);
        }
        if(movementInput.x < 0){
            SpriteRenderer.flipX = true;
        } else if (movementInput.x > 0) {
            SpriteRenderer.flipX = false;
        }

    }

    bool TryMove(Vector2 direction){
        if(direction != Vector2.zero){
    
        int count = rb.Cast( 
            direction,
            movementFilter,
            castCollisions, 
            moveSpeed * Time.fixedDeltaTime + collisionOffset);

            if(count == 0){  
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime );
                return true;
            } else { 
                return false; 
            }

            } else { 
                return false; 
            }
    }
void OnMove(InputValue movementValue) {
    movementInput = movementValue.Get<Vector2>();

    }
}
