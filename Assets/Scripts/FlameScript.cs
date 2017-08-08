using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameScript : MonoBehaviour {

    public float Timer = 1f;

    private List<GameObject> collided = new List<GameObject>();

    private void OnEnable() {
        StartCoroutine(TimeFlame());
    }

    private IEnumerator TimeFlame() {
        yield return new WaitForSeconds(this.Timer);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// When a flame touch something
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision) {
        if (this.collided.Contains(collision.gameObject)) return;
        this.collided.Add(collision.gameObject);
        PlayerController pc;
        BombScript bs;
        if (pc = collision.gameObject.GetComponent<PlayerController>()) {
            pc.TouchByExplosion();
        }
        if (bs = collision.gameObject.GetComponent<BombScript>()) {
            bs.Explode();
        }
    }
}
