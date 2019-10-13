using UnityEngine;

enum MoveDirection
{
    Left = -1,
    Right = 1,
}

class BackgroundController
{
    private GameObject background;
    private GameObject helpingBackground;
    private readonly Camera mainCamera;
    private readonly float moveMultiplier;
    private readonly float repositionDelta;
    private readonly float backgroundHalfWidth;
    private readonly Vector2 backgroundMoveDirection = new Vector2(1, -1);
    private Vector2 previousCameraPosition;

    public BackgroundController(
        GameObject background, 
        GameObject helpingBackground, 
        Camera mainCamera,
        float moveMultiplier,
        float repositionDelta)
    {
        this.background = background;
        this.helpingBackground = helpingBackground;
        this.mainCamera = mainCamera;
        this.moveMultiplier = moveMultiplier;
        this.repositionDelta = repositionDelta;
        
        var backgroundRenderer = background.GetComponent<Renderer>();
        backgroundHalfWidth = backgroundRenderer.bounds.extents.x;

        previousCameraPosition = mainCamera.transform.position;
    }
    public void UpdateTrigger()
    {
        Vector2 currentCameraPosition = mainCamera.transform.position;

        var backgroundTranslation =
            (currentCameraPosition - previousCameraPosition) *
            backgroundMoveDirection * 
            moveMultiplier;

        previousCameraPosition = currentCameraPosition;

        background.transform.Translate(backgroundTranslation);
        helpingBackground.transform.Translate(backgroundTranslation);

        var halfCamWidth = mainCamera.aspect * mainCamera.orthographicSize;
        var camLeftXBound = currentCameraPosition.x - halfCamWidth;
        var camRightXBound = currentCameraPosition.x + halfCamWidth;
        var spriteLeftXBound = background.transform.position.x - backgroundHalfWidth;
        var spriteRightXBound = background.transform.position.x + backgroundHalfWidth;
        if (camRightXBound + repositionDelta > spriteRightXBound)
        {
            RepositionBackground(MoveDirection.Right);
        }
        else if (camLeftXBound - repositionDelta < spriteLeftXBound)
        {
            RepositionBackground(MoveDirection.Left);
        }
    }

    private void RepositionBackground(MoveDirection direction)
    {
        var currentBkgPosition = background.transform.position;

        helpingBackground.transform.position = new Vector2(
            currentBkgPosition.x + backgroundHalfWidth * 2 * (int)direction, 
            helpingBackground.transform.position.y);

        var temp = background;
        background = helpingBackground;
        helpingBackground = temp;
    }
}