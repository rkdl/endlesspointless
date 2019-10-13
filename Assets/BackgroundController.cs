using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

enum MoveDirection
{
    Left = -1,
    Right = 1,
}

public class BackgroundController : MonoBehaviour
{
    public float moveMultiplier = 0.25f;
    public float backgroundRepositionDelta = 0.1f;
    public GameObject background;
    public GameObject middleground;
    
    private GameObject helpingBackground;
    private float backgroundHalfWidth;
    
    private readonly Vector2 backgroundMoveDirection = new Vector2(1, -1);
    private Vector2 previousCameraPosition;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        previousCameraPosition = mainCamera.transform.position;

        var backgroundRenderer = background.GetComponent<Renderer>();
        backgroundHalfWidth = backgroundRenderer.bounds.extents.x;

        var currentBkgPosition = background.transform.position;
        var bkgSpriteWidth = backgroundHalfWidth * 2;
        
        helpingBackground = Instantiate(
            background, 
            new Vector2(currentBkgPosition.x - bkgSpriteWidth, currentBkgPosition.y), 
            background.transform.rotation);
        helpingBackground.transform.parent = background.transform.parent;
    }

    void Update()
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
        if (camRightXBound + backgroundRepositionDelta > spriteRightXBound)
        {
            RepositionBackground(MoveDirection.Right);
        }
        else if (camLeftXBound - backgroundRepositionDelta < spriteLeftXBound)
        {
            RepositionBackground(MoveDirection.Left);
        }
    }

    private void RepositionBackground(MoveDirection direction)
    {
        var currentBkgPosition = background.transform.position;
        var bkgSpriteWidth = backgroundHalfWidth * 2;

        helpingBackground.transform.position = new Vector2(
            currentBkgPosition.x + bkgSpriteWidth * (int)direction, 
            helpingBackground.transform.position.y);

        var temp = background;
        background = helpingBackground;
        helpingBackground = temp;
    }
}
