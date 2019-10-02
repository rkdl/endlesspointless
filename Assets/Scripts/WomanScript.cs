using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WomanScript : MonoBehaviour
{
    private const float ROTATION_PERIOD = 30;
    private float prevRotationTime = 0f;

    void Update()
    {
        var thisMoment = Time.time;
        if (thisMoment - prevRotationTime >= ROTATION_PERIOD)
        {
            CharacterUtils.TurnAround(transform);
            prevRotationTime = thisMoment;
        }
    }
}
