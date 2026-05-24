using System.Collections.Generic;
using UnityEngine;

public class Lightsaber : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform tip;
    [SerializeField] private Transform bladeBase;
    [SerializeField] private MeshFilter meshFilter;

    [Header("Trail Settings")]
    [SerializeField] private int maxPoints = 20;
    [SerializeField] private float minVertexDistance = 0.03f;
    [SerializeField] private float pointLifeTime = 0.08f;

    private Mesh trailMesh;

    private class TrailPoint
    {
        public Vector3 tipPosition;
        public Vector3 basePosition;
        public float lifeTime;
    }

    private readonly List<TrailPoint> points = new();

    private void Start()
    {
        trailMesh = new Mesh();
        trailMesh.name = "Lightsaber Trail";

        meshFilter.mesh = trailMesh;
    }

    private void LateUpdate()
    {
        RemoveOldPoints();

        AddPoint();

        BuildMesh();
    }

    private void RemoveOldPoints()
    {
        float currentTime = Time.time;

        points.RemoveAll(point =>
            currentTime - point.lifeTime > pointLifeTime);
    }

    private void AddPoint()
    {
        Vector3 currentTip = tip.position;

        if (points.Count > 0)
        {
            float distance =
                Vector3.Distance(points[0].tipPosition, currentTip);

            if (distance < minVertexDistance)
                return;
        }

        TrailPoint point = new TrailPoint
        {
            tipPosition = tip.position,
            basePosition = bladeBase.position,
            lifeTime = Time.time
        };

        points.Insert(0, point);

        if (points.Count > maxPoints)
        {
            points.RemoveAt(points.Count - 1);
        }
    }

    private void BuildMesh()
    {
        if (points.Count < 2)
        {
            trailMesh.Clear();
            return;
        }

        int vertexCount = points.Count * 2;
        int triangleCount = (points.Count - 1) * 6;

        Vector3[] vertices = new Vector3[vertexCount];
        Vector2[] uvs = new Vector2[vertexCount];
        int[] triangles = new int[triangleCount];

        for (int i = 0; i < points.Count; i++)
        {
            TrailPoint point = points[i];

            Vector3 localBase =
                transform.InverseTransformPoint(point.basePosition);

            Vector3 localTip =
                transform.InverseTransformPoint(point.tipPosition);

            vertices[i * 2] = localBase;
            vertices[i * 2 + 1] = localTip;

            float t = (float)i / (points.Count - 1);

            uvs[i * 2] = new Vector2(t, 0);
            uvs[i * 2 + 1] = new Vector2(t, 1);
        }

        int triangleIndex = 0;

        for (int i = 0; i < points.Count - 1; i++)
        {
            int vertIndex = i * 2;

            triangles[triangleIndex++] = vertIndex;
            triangles[triangleIndex++] = vertIndex + 1;
            triangles[triangleIndex++] = vertIndex + 2;

            triangles[triangleIndex++] = vertIndex + 2;
            triangles[triangleIndex++] = vertIndex + 1;
            triangles[triangleIndex++] = vertIndex + 3;
        }

        trailMesh.Clear();

        trailMesh.vertices = vertices;
        trailMesh.uv = uvs;
        trailMesh.triangles = triangles;

        trailMesh.RecalculateBounds();
        trailMesh.RecalculateNormals();
    }
}