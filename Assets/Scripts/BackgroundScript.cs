using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public float backgroundFollowMultiplier = 0.25f;
    public float middlegroundFollowMultiplier = 0.5f;
    public float repositionDelta = 0.1f;
    public GameObject background;
    public GameObject middleground;

    private BackgroundController backgroundController;
    private BackgroundController middlegroundController;

    private void Start()
    {
        backgroundController = new BackgroundController(
            background,
            CloneBackground(background),
            Camera.main,
            backgroundFollowMultiplier,
            repositionDelta);

        middlegroundController = new BackgroundController(
            middleground,
            CloneBackground(middleground),
            Camera.main,
            middlegroundFollowMultiplier,
            repositionDelta);
    }

    private GameObject CloneBackground(GameObject bkg)
    {
        var currentBkgPosition = bkg.transform.position;
        var bkgSpriteWidth = bkg.GetComponent<Renderer>().bounds.extents.x;

        var helpingBackground = Instantiate(
            bkg,
            new Vector2(currentBkgPosition.x - bkgSpriteWidth, currentBkgPosition.y),
            bkg.transform.rotation);
        helpingBackground.transform.parent = bkg.transform.parent;

        return helpingBackground;
    }

    private void LateUpdate()
    {
        backgroundController.UpdateTrigger();
        middlegroundController.UpdateTrigger();
    }
}
