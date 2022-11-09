using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputListener
{
    #region Fields
    public Action OnInputStarted;
    public Action OnInputEnded;
    private bool isDragging;
    private Vector2 beganPos;
    private Vector2 currentPos;
    private Vector2 endPos;
    private Vector2 deltaPos;
    private Vector2 deltaNormal;
    #endregion

    #region Getters
    public bool IsDragging => isDragging;
    public Vector3 GetMouseWorldPos(Camera gameCam, Camera screenCam)
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenCam.farClipPlane);
        return gameCam.ScreenToWorldPoint(mousePos);
    }
    #endregion

    #region Core
    public InputListener()
    {
    }
    private void clearInputs()
    {
        isDragging = false;
        beganPos = Vector2.zero;
        currentPos = Vector2.zero;
        endPos = Vector2.zero;
        deltaPos = Vector2.zero;
        deltaNormal = Vector2.zero;
    }
    #endregion

    #region Input
    private void onMouseDown()
    {
        beganPos = UnityEngine.Input.mousePosition;

        OnInputStarted?.Invoke();
    }
    private void onDrag()
    {
        currentPos = UnityEngine.Input.mousePosition;

        deltaPos = currentPos - beganPos;
        deltaNormal = deltaPos.normalized;

        isDragging = deltaNormal.magnitude > .1f ? true : false;
    }
    private void onMouseUp()
    {
        endPos = UnityEngine.Input.mousePosition;

        OnInputEnded?.Invoke();

        clearInputs();
    }
    public void ExternalUpdate()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            onMouseDown();
        }
        if (UnityEngine.Input.GetMouseButton(0))
        {
            onDrag();
        }
        if (UnityEngine.Input.GetMouseButtonUp(0))
        {
            onMouseUp();
        }
    }
    #endregion
}
