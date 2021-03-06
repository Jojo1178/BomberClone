﻿using System.Collections;
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

    public Rigidbody2D rigidBody;
    public Animator animator;

    /*Sounds*/
    public AudioClip[] sound_bomb_drop;
    public AudioClip[] sound_player_death;

    private bool isAlive = true;

    private void LateUpdate() {
        if (Input.GetKeyUp(this.BombKey)) {
            this.CanDropBombs = true;
        }
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
            GameObject bomb = Instantiate(bombPrefab);
            Vector3 position = this.transform.localPosition;
            bomb.transform.localPosition = new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), Mathf.Round(position.z));
            this.CanDropBombs = false;

            //Play drop bomb sound:
            SoundManager.instance.RandomizeSfx(sound_bomb_drop);
        }
    }

    /// <summary>
    /// When an explosion touch the player
    /// </summary>
    public void TouchByExplosion() {

        if (isAlive)
        {
            this.isAlive = false;

            //TODO : Play Death Animation

            //Play player death sound:
            SoundManager.instance.RandomizeSfx(sound_player_death);

            Debug.Log(this.gameObject.name + " has been DELETED");
            Destroy(this.gameObject);

            //Inform the Application Controller that the player died:
            GameObject appController = GameObject.Find("AppController");
            VictoryManager victoryManager = appController.GetComponent<VictoryManager>();
            victoryManager.playerDied();
        }
    }

    public Rigidbody2D getRigidBody()
    {
        return rigidBody;
    }
}