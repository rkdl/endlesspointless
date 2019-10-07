using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float moveMultiplier = 0.25f;

    private readonly Vector2 backgroundMoveDirection = new Vector2(1, -1);
    private Vector2 previousCameraPosition;
    private Camera mainCamera;
    private Sprite sprite;
    
    private Vector2 screenBounds;
    void Start()
    {
        mainCamera = Camera.main;
        sprite = GetComponent<Sprite>();
        previousCameraPosition = mainCamera.transform.position;
    }

    void Update()
    {
        Vector2 currentCameraPosition = mainCamera.transform.position;

        var backgroundTranslation =
            (currentCameraPosition - previousCameraPosition) *
            backgroundMoveDirection * 
            moveMultiplier;
        transform.Translate(backgroundTranslation);

        var halfCamWidth = mainCamera.aspect * mainCamera.orthographicSize;
        var camLeftXBound = currentCameraPosition.x - halfCamWidth;
        var camRightXBound = currentCameraPosition.x + halfCamWidth;

        previousCameraPosition = mainCamera.transform.position;
        
        var renderer = GetComponent<Renderer>();

        var spriteLeftXBound = renderer.transform.position.x - renderer.bounds.extents.x;
        var spriteRightXBound = renderer.transform.position.x + renderer.bounds.extents.x;
        var delta = 0.1f;
        if (camRightXBound + delta > spriteRightXBound)
        {
            // need to spawn texture right
        }
        else if (camLeftXBound - delta < spriteLeftXBound)
        {
            // spawn texture left
        }
    }
}
