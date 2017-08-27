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
        int dir = 1;
        GameObject flame = Instantiate(this.FlamePrefab);
        flame.transform.localPosition = this.transform.localPosition;
        while (dir < this.Force) {
            flame = Instantiate(this.FlamePrefab);
            flame.transform.localPosition = this.transform.localPosition + Vector3.left * dir;
            flame = Instantiate(this.FlamePrefab);
            flame.transform.localPosition = this.transform.localPosition + Vector3.right * dir;
            flame = Instantiate(this.FlamePrefab);
            flame.transform.localPosition = this.transform.localPosition + Vector3.up * dir;
            flame = Instantiate(this.FlamePrefab);
            flame.transform.localPosition = this.transform.localPosition + Vector3.down * dir;
            dir++;
        }

        Destroy(this.gameObject);
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
