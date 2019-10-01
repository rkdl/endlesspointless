using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomanScript : MonoBehaviour
{
    private const float ROTATION_PERIOD = 30;
    private float prevRotationTime = 0f;

    // Update is called once per frame
    void Update()
    {
        var thisMoment = Time.time;
        if (thisMoment - prevRotationTime >= ROTATION_PERIOD)
        {
            TurnAround();
            prevRotationTime = thisMoment;
        }
    }

    private void TurnAround()
    {
        var currentAngles = gameObject.transform.localEulerAngles;

        var newY = currentAngles.y < 180 ? currentAngles.y + 180 : currentAngles.y - 180;
        gameObject.transform.localEulerAngles = new Vector3(currentAngles.x, newY);
    }
}
