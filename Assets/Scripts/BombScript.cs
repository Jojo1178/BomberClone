using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour {

    public float Timer = 3f;
    public int Force = 3;

    public GameObject FlamePrefab;
    public BoxCollider2D Collider;

    private bool creatorLeft;

    /*Sounds*/
    public AudioClip[] sound_bomb_explosion;

    private void Start()
    {
        //Send the bomb location to the IAIntelligence:
        IAIntelligence.INSTANCE.addBombToMap(Mathf.Round(this.transform.localPosition.x), Mathf.Round(this.transform.localPosition.y));
    }

    private void OnEnable() {
        this.Collider.enabled = false;
        this.creatorLeft = false;
        StartCoroutine(TimeBomb());
    }

    /// <summary>
    /// When a bomb explode
    /// </summary>
    public void Explode() {
        
        //Play bomb explosion sound:
        SoundManager.instance.RandomizeSfx(sound_bomb_explosion);

        StopCoroutine(TimeBomb());
        //int dir = 1;
        GameObject flame = Instantiate(this.FlamePrefab);
        flame.transform.localPosition = this.transform.localPosition;
        FlameScript fs = flame.GetComponent<FlameScript>();
        fs.Init(0,Vector3.zero, false);

        CreateFlame(this.transform.localPosition , Vector3.left);
        CreateFlame(this.transform.localPosition , Vector3.right);
        CreateFlame(this.transform.localPosition , Vector3.up);
        CreateFlame(this.transform.localPosition , Vector3.down);

        Destroy(this.gameObject);

        //Remove the bomb location from the map used by IAIntelligence:
        IAIntelligence.INSTANCE.removeEntityFromMap(Mathf.Round(this.transform.localPosition.x), Mathf.Round(this.transform.localPosition.y), "BOMB");
    }

    private void CreateFlame(Vector3 position,Vector3 direction) {
        RaycastHit2D hit = Physics2D.Raycast(position + direction, Vector2.zero);

        bool breakable = true, stop = false;
        if (hit.collider != null) {
            WallScript ws;
            if (ws = hit.collider.gameObject.GetComponent<WallScript>()) {
                stop = true;
                breakable = ws.breakable;
                ws.DestroyWall();
            }
        }
        if (breakable)
        {
            GameObject flame = Instantiate(this.FlamePrefab);
            flame.transform.localPosition = this.transform.localPosition;
            FlameScript fs = flame.GetComponent<FlameScript>();
            fs.Init(this.Force - 1, direction, stop);
        }
    }

    /// <summary>
    /// Bomb timer
    /// </summary>
    private IEnumerator TimeBomb() {
        //TODO : Play Bomb animation
        yield return new WaitForSeconds(this.Timer);
        Explode();
    }

    /// <summary>
    /// Allow bomb placer to leave before activating bomb's collider
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision) {
        if (!creatorLeft) {
            creatorLeft = true;
            this.Collider.enabled = true;
        }

    }

}
