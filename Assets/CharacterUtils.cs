using UnityEngine;

public static class CharacterUtils
{
    public static void TurnAround(Transform transform)
    {
        var currentAngles = transform.localEulerAngles;
        var newY = currentAngles.y < 180 ? currentAngles.y + 180 : currentAngles.y - 180;
        transform.localEulerAngles = new Vector3(currentAngles.x, newY);
    }
}
