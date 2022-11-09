using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DevShirme.PlayerModule
{
    [Serializable]
    public class Pencil
    {
        #region Fields
        [Header("Pencil Fields")]
        [SerializeField] private MeshCollider drawQuad;
        [SerializeField] private Camera gameCamera;
        //----------------------------------------
        private bool isDragging;
        private Vector2 beganPos;
        private Vector2 currentPos;
        private Vector2 endPos;
        private Vector2 deltaPos;
        private Vector2 deltaNormal;
        private bool canDraw;
        #endregion

        #region Core
        public void ClearInputs()
        {
            isDragging = false;
            canDraw = false;
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
            ClearInputs();
        }
        public void ExternalUpdate()
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            canDraw = drawQuad.bounds.Contains(gameCamera.ScreenToWorldPoint(mousePos));
            if (canDraw)
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
            
        }
        #endregion
    }
}

