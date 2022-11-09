using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pencil
{
    #region Fields
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
    public Vector2 BeganPos => beganPos;
    public Vector2 CurrentPos => currentPos;
    public Vector2 EndPos => endPos;
    public Vector2 DeltaPos => deltaPos;
    public Vector2 DeltaNormal => deltaNormal;
    #endregion

    #region Core
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
        beganPos = Input.mousePosition;
    }
    private void onDrag()
    {
        currentPos = Input.mousePosition;

        deltaPos = currentPos - beganPos;
        deltaNormal = deltaPos.normalized;

        isDragging = deltaNormal.magnitude > .1f ? true : false;
    }
    private void onMouseUp()
    {
        endPos = Input.mousePosition;

        OnInputEnded?.Invoke();
    }
    public void ExternalUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onMouseDown();
        }
        if (Input.GetMouseButton(0))
        {
            onDrag();
        }
        if (Input.GetMouseButtonUp(0))
        {
            onMouseUp();
        }
    }
    #endregion
}
