using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {

    public bool breakable = true;

	public void DestroyWall() {
        if(this.breakable)
            Destroy(this.gameObject);
    }
}
