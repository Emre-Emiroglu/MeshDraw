using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingController : MonoBehaviour
{
    #region Fields
    [Header("Drawing Settings")]
    [SerializeField] private float threshold = .1f;
    [SerializeField] private GameObject drawResult;
    [Header("Components")]
    [SerializeField] private LineRenderer lr;
    [SerializeField] private MeshCollider drawQuad;
    [Header("Cameras")]
    [SerializeField] private Camera gameCamera;
    [SerializeField] private Camera screenCamera;
    private InputListener inputListener;
    private bool canDraw;
    private List<Vector3> fingerPoses;
    #endregion

    #region Core
    private void Awake()
    {
        inputListener = new InputListener();
        inputListener.OnInputStarted += onInputStarted;
        inputListener.OnInputEnded += onInputEnded;
        fingerPoses = new List<Vector3>();
    }
    private void OnDestroy()
    {
        inputListener.OnInputStarted -= onInputStarted;
        inputListener.OnInputEnded -= onInputEnded;
    }
    #endregion

    #region Receivers
    private void onInputStarted()
    {
        lr.positionCount = 2;
        Vector3 pos = inputListener.GetMouseWorldPos(gameCamera, screenCamera);
        for (int i = 0; i < 2; i++)
        {
            fingerPoses.Add(pos);
            lr.SetPosition(i, fingerPoses[i]);
        }
    }
    private void onInputEnded()
    {
        lineToMesh();
        fingerPoses.Clear();
        lr.positionCount = 0;
    }
    #endregion

    #region Executes
    private void drawLine()
    {
        Vector3 mouseWorldPos = inputListener.GetMouseWorldPos(gameCamera, screenCamera);
        float diff = Mathf.Sqrt(Vector3.SqrMagnitude(mouseWorldPos - fingerPoses[fingerPoses.Count - 1]));
        if (diff > threshold)
        {
            fingerPoses.Add(mouseWorldPos);
            lr.positionCount++;
            lr.SetPosition(lr.positionCount - 1, mouseWorldPos);
        }
    }
    private void lineToMesh()
    {
        Mesh mesh = new Mesh();
        lr.BakeMesh(mesh, true);
        drawResult.GetComponent<MeshFilter>().sharedMesh = mesh;
        drawResult.GetComponent<MeshCollider>().sharedMesh = mesh;
        drawResult.GetComponent<MeshCollider>().convex = true;

        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] += Vector3.up * 3;
        }
        mesh.vertices = vertices;

        mesh.RecalculateBounds();
        mesh.Optimize();
    }
    #endregion

    #region Updates
    public void Update()
    {
        canDraw = drawQuad.bounds.Contains(inputListener.GetMouseWorldPos(gameCamera, screenCamera));
        if (canDraw)
        {
            inputListener.ExternalUpdate();
            if (inputListener.IsDragging)
            {
                drawLine();
            }
        }
    }
    #endregion
}
