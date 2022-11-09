using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingController : MonoBehaviour
{
    #region Fields
    [Header("Drawing Controller Fields")]
    [SerializeField] private LineRenderer lr;
    [SerializeField] private MeshCollider drawQuad;
    [SerializeField] private Camera gameCamera;
    [SerializeField] private Camera screenCamera;
    private Pencil pencil;
    private bool canDraw;
    #endregion

    #region Core
    private void Awake()
    {
        pencil.OnInputEnded += onInputEnded;   
    }
    private void OnDestroy()
    {
        pencil.OnInputEnded -= onInputEnded;
    }
    #endregion

    #region Receivers
    private void onInputEnded()
    {
        lineToMesh();
    }
    #endregion

    #region Executes
    private void drawLine()
    {
    }
    private void lineToMesh()
    {
    }
    #endregion

    #region Updates
    public void Update()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenCamera.farClipPlane);
        canDraw = drawQuad.bounds.Contains(gameCamera.ScreenToWorldPoint(mousePos));
        if (canDraw)
        {
            pencil.ExternalUpdate();
            if (pencil.IsDragging)
            {
                drawLine();
            }
        }
    }
    #endregion
}
