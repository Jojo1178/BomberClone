using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour {

    public float Timer = 3f;
    public BoxCollider2D Collider;
    private bool CreatorLeft;

    private void OnEnable() {
        this.Collider.enabled = false;
        this.CreatorLeft = false;
        StartCoroutine(TimeBomb());
    }

    private IEnumerator TimeBomb() {
        //TODO : Play Bomb animation
        yield return new WaitForSeconds(this.Timer);
        Explode();
    }

    private void Explode() {
        //TODO : Explotion stuff
        Destroy(this.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (!CreatorLeft) {
            CreatorLeft = true;
            this.Collider.enabled = true;
        }

    }

}
