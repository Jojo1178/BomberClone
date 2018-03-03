using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameScript : MonoBehaviour {

    public float Timer = 1f;

    private List<GameObject> collided = new List<GameObject>();
    private GameObject lightGameObject;

    private void Start()
    {
        //Send the flame location to the IAIntelligence:
        IAIntelligence.INSTANCE.addFlameToMap(Mathf.Round(this.transform.localPosition.x), Mathf.Round(this.transform.localPosition.y));
    }

    private void OnEnable() {
        StartCoroutine(TimeFlame());
    }

    private IEnumerator TimeFlame() {
        yield return new WaitForSeconds(this.Timer);
        Destroy(this.gameObject);
        Destroy(this.lightGameObject);

        //Remove the flame location from the map used by IAIntelligence:
        IAIntelligence.INSTANCE.removeEntityFromMap(Mathf.Round(this.transform.localPosition.x), Mathf.Round(this.transform.localPosition.y), "FLAME");
    }

    /// <summary>
    /// When a flame touch something
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision) {
        if (this.collided.Contains(collision.gameObject)) return;
        this.collided.Add(collision.gameObject);
        IAController iac;
        PlayerController pc;
        BombScript bs;
        if (pc = collision.gameObject.GetComponent<PlayerController>()) {
            pc.TouchByExplosion();
        }
        if (bs = collision.gameObject.GetComponent<BombScript>()) {
            bs.Explode();
        }
        if(iac = collision.gameObject.GetComponent<IAController>())
        {
            iac.TouchByExplosion();
        }
    }

    public void Init(int force, Vector3 direction, bool stop)
    {
        this.transform.localPosition += direction;
        if (!stop && force > 0)
        {

            RaycastHit2D hit = Physics2D.Raycast(this.transform.localPosition + direction, Vector2.zero);
            bool breakable = true;
            if (hit.collider != null)
            {
                WallScript ws;
                if (ws = hit.collider.gameObject.GetComponent<WallScript>())
                {
                    stop = true;
                    breakable = ws.breakable;
                    ws.DestroyWall();
                }
            }
            if (breakable)
            {
                GameObject flame = Instantiate(this.gameObject);
                flame.transform.localPosition = this.transform.localPosition;
                FlameScript fs = flame.GetComponent<FlameScript>();
                fs.Init(force - 1, direction, stop);
            }


            //Create a Light
            lightGameObject = new GameObject("Flame Light");

            //Set Light properties:
            Light lightComp = lightGameObject.AddComponent<Light>();
            lightComp.color = Color.white;
            lightComp.range = 3;
            lightComp.intensity = 7;

            //Set the position of the light:
            Vector3 lightPosition = this.transform.localPosition;
            lightPosition.z = -1;
            lightGameObject.transform.position = lightPosition;
        }
    }
}
