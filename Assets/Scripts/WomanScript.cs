using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class WomanScript : MonoBehaviour
{
    public float rotationPeriod = 30;

    private float prevRotationTime = 0f;

    void Update()
    {
        var thisMoment = Time.time;
        if (thisMoment - prevRotationTime >= rotationPeriod)
        {
            CharacterUtils.TurnAround(transform);
            prevRotationTime = thisMoment;
        }
    }
}
