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

    private void OnEnable() {
        this.Collider.enabled = false;
        this.creatorLeft = false;
        StartCoroutine(TimeBomb());
    }

    /// <summary>
    /// When a bomb explode
    /// </summary>
    public void Explode() {
        StopCoroutine(TimeBomb());
        //int dir = 1;
        GameObject flame = Instantiate(this.FlamePrefab);
        flame.transform.localPosition = this.transform.localPosition;
        FlameScript fs = flame.GetComponent<FlameScript>();
        fs.Init(0,Vector3.zero);

        CreateFlame(this.transform.localPosition , Vector3.left);
        CreateFlame(this.transform.localPosition , Vector3.right);
        CreateFlame(this.transform.localPosition , Vector3.up);
        CreateFlame(this.transform.localPosition , Vector3.down);
        
        Destroy(this.gameObject);
    }

    private void CreateFlame(Vector3 position,Vector3 direction) {
        RaycastHit2D hit = Physics2D.Raycast(position + direction, Vector2.zero);

        bool breakable = false, stop = false;
        if (hit.collider != null) {
            WallScript ws;
            if (ws = hit.collider.gameObject.GetComponent<WallScript>()) {
                stop = true;
                breakable = ws.breakable;
                ws.DestroyWall();
            }
        }
        if (!stop || breakable) {
            GameObject flame = Instantiate(this.FlamePrefab);
            flame.transform.localPosition = this.transform.localPosition;
            FlameScript fs = flame.GetComponent<FlameScript>();
            if (!stop)
                fs.Init(this.Force - 1, direction);
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
