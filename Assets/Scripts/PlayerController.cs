using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    /*Keyboard controls*/
    public KeyCode UpKey;
    public KeyCode LeftKey;
    public KeyCode RightKey;
    public KeyCode DownKey;
    public KeyCode BombKey;

    /*Speed*/
    public float MaxSpeed = 10f;

    public bool CanDropBombs = true; //Can the player drop bombs?
    public bool CanMove = true; //Can the player move?

    /*Prefabs*/
    public GameObject bombPrefab;

    private Rigidbody2D rigidBody;
    private Transform playerTransform;
    private Animator animator;

    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        playerTransform = base.transform;
        animator = playerTransform.GetComponent<Animator>();
    }

    private void FixedUpdate() {
        Vector2 movement = Vector2.zero;
        if (Input.GetKey(this.UpKey)) { //Up movement
            movement.y = MaxSpeed;
        }
        if (Input.GetKey(this.LeftKey)) { //Left movement
            movement.x = -MaxSpeed;
        }
        if (Input.GetKey(this.RightKey)) { //Right movement
            movement.x = MaxSpeed;
        }
        if (Input.GetKey(this.DownKey)) { //Down movement
            movement.y = -MaxSpeed;
        }
        animator.SetFloat("SpeedX", this.rigidBody.velocity.x);
        animator.SetFloat("SpeedY", this.rigidBody.velocity.y);
        this.rigidBody.velocity = movement;
        if (this.CanDropBombs && Input.GetKey(this.BombKey)) { //Drop bomb
            DropBomb();
        }
    }

    /// <summary>
    /// Drops a bomb beneath the player
    /// </summary>
    private void DropBomb() {
        if (bombPrefab) { //Check if bomb prefab is assigned first

        }
    }
}
