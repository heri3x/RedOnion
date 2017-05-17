using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShakeController : MonoBehaviour
{
    public void Init()
    {
    }

    public void Term()
    {
    }

    public void Shake(GameObject shakeObj)
    {
        iTween.ShakePosition(shakeObj, iTween.Hash("x", 0.3f, "y", 0.3f, "time", 0.5f));
    }
}
