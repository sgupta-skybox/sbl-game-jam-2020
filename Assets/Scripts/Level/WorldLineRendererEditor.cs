using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WorldLineRendererEditor : MonoBehaviour
{
    // Start is called before the first frame update
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();

        if (Application.isPlaying)
            enabled = false;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            if (lineRenderer && edgeCollider)
            {
                lineRenderer.positionCount = edgeCollider.points.Length;
                lineRenderer.SetPositions(edgeCollider.points.ToVec3Array());
            }
        }
#endif
    }

}
