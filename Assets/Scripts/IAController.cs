using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAController : MonoBehaviour
{
    /* Gameplay */
    public float timerBetweenTwoBombs = 5f;

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

    private Vector2 objective;
    private Vector2 previousPosition;
    private bool offensiveMode = true;

    private bool isAlive = true;

    private void Awake()
    {
        objective = this.transform.position;
        previousPosition = objective;
        offensiveMode = IAIntelligence.INSTANCE.getObjective(this.transform.position, ref this.objective, this.previousPosition);
    }

    private void FixedUpdate()
    {
        if (isOjectiveReached())
        {
            this.transform.position = this.objective;
            
            //Drop bomb
            int drop = UnityEngine.Random.Range(1, 4);
            if (this.CanDropBombs && offensiveMode && drop == 2)
            {
                DropBomb();
            }
            
            //Appeller l'IA:
            offensiveMode = IAIntelligence.INSTANCE.getObjective(this.transform.position, ref this.objective, this.previousPosition);

            //On enregistre la previousPosition avant de deplacer l'IA:
            this.previousPosition = this.transform.position;
        }
        

        //On déplace l'IA vers l'objectif avec la vitesse voulue:
        Vector2 dir = this.objective - (Vector2)this.transform.position;
        Vector2 movement = dir * MaxSpeed;

        animator.SetFloat("SpeedX", this.rigidBody.velocity.x);
        animator.SetFloat("SpeedY", this.rigidBody.velocity.y);
        this.rigidBody.velocity = movement;
    }

    private bool isOjectiveReached()
    {
        return Vector2.Distance(this.objective, (Vector2)this.transform.position) < .01f;
    }

    /// <summary>
    /// Drops a bomb beneath the player
    /// </summary>
    private void DropBomb()
    {
        if (bombPrefab)
        { //Check if bomb prefab is assigned first
            GameObject bomb = Instantiate(bombPrefab);
            Vector3 position = this.transform.localPosition;
            bomb.transform.localPosition = new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), Mathf.Round(position.z));
            this.CanDropBombs = false;

            //Play drop bomb sound:
            SoundManager.instance.RandomizeSfx(sound_bomb_drop);

            //When a bomb is planted, we launch a timer to wait before another bomb plant:
            StartCoroutine(timeBetweenTwoBombs());
        }
    }

    /// <summary>
    /// When an explosion touch the player
    /// </summary>
    public void TouchByExplosion()
    {
        if (isAlive)
        {
            this.isAlive = false;

            //TODO : Play Death Animation

            //Play player death sound:
            SoundManager.instance.RandomizeSfx(sound_player_death);

            Debug.Log(this.gameObject.name + " has been DELETED");
            Destroy(this.gameObject);

            //Inform the Application Controller that the ia died:
            GameObject appController = GameObject.Find("AppController");
            VictoryManager victoryManager = appController.GetComponent<VictoryManager>();
            victoryManager.iaDied();
        }
    }

    private IEnumerator timeBetweenTwoBombs()
    {
        yield return new WaitForSeconds(this.timerBetweenTwoBombs);
        this.CanDropBombs = true;
    }
}