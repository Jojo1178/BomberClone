using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {

    public bool breakable = true;

	public void DestroyWall() {
        if(this.breakable)
        {
            Destroy(this.gameObject);

            //Remove the wall location from the map used by IAIntelligence:
            IAIntelligence.INSTANCE.removeEntityFromMap(Mathf.Round(this.transform.localPosition.x), Mathf.Round(this.transform.localPosition.y), "DESTROYED WALL");
        }
    }
}
