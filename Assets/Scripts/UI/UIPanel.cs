using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIPanel : MonoBehaviour
{
    public abstract void onActivationAction();
    public abstract void onClickAction(string buttonName);
}
