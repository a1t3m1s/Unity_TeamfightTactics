using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class HexRenderer : MonoBehaviour
{
    private Mesh mesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private List<Face> faces;

    [field: SerializeField]
    private const string meshName = "HexTile";
    private const int sidesCount = 6;

    public Material material;
    public float innerSize = 1.3f;
    public float outerSize = 1.0f;
    public float height = 0.2f;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        mesh = new Mesh();
        mesh.name = meshName;

        meshFilter.mesh = mesh;
        meshRenderer.material = material;
    }

    private void OnEnable()
    {
        DrawMesh();
    }

    public void DrawMesh()
    {
        DrawFaces();
        CombineFaces();
    }

    private void DrawFaces()
    {
        faces = new List<Face>();

        //top faces
        for(int point=0; point < sidesCount; ++point)
        {
            faces.Add(CreateFace(innerSize, outerSize, height / 2f, height / 2f, point));
        }

        //bottom faces
        for(int point = 0; point < sidesCount; ++point)
        {
            faces.Add(CreateFace(innerSize, outerSize, -height / 2f, -height / 2f, point, true));
        }

        //outer faces
        for(int point = 0; point < sidesCount; ++point)
        {
            faces.Add(CreateFace(outerSize, outerSize, height / 2f, -height / 2f, point, true)); 
        }

        //outer faces
        for(int point = 0; point < sidesCount; ++point)
        {
            faces.Add(CreateFace(innerSize, innerSize, height / 2f, -height / 2f, point, false));
        }
    }

    private void CombineFaces()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for(int i=0; i<faces.Count; ++i)
        {
            vertices.AddRange(faces[i].vertices);
            uvs.AddRange(faces[i].uvs);

            int offset = (4 * i);
            foreach(int triangle in faces[i].triangles)
            {
                tris.Add(triangle + offset);
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
    }

    private Face CreateFace(float innerRad, float outerRad, float heightA, float heightB, int point, bool reverse = false)
    {
        Vector3 pointA = GetPoint(innerRad, heightB, point);
        Vector3 pointB = GetPoint(innerRad, heightB, (point<5)?point+1:0);
        Vector3 pointC = GetPoint(outerRad, heightA, (point<5)?point+1:0);
        Vector3 pointD = GetPoint(outerRad, heightA, point);

        List<Vector3> vertices = new List<Vector3>() { pointA, pointB, pointC, pointD};
        List<int> triangles = new List<int> { 0, 1, 2, 2, 3, 0 };
        List<Vector2> uvs = new List<Vector2>() { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1)};

        if(reverse)
        {
            vertices.Reverse();
        }

        return new Face(vertices, triangles, uvs);
    }

    private Vector3 GetPoint(float size, float height, int index)
    {
        float angleDeg = 60 * index;
        float angleRad = Mathf.PI / 180f * angleDeg;
        return new Vector3((size * Mathf.Cos(angleRad)), height, size*Mathf.Sin(angleRad));
    }
}

public struct Face
{
    public List<Vector3> vertices { get; private set; }
    public List<int> triangles { get; private set; }
    public List<Vector2> uvs { get; private set; }

    public Face(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs)
    {
        this.vertices = vertices;
        this.triangles = triangles;
        this.uvs = uvs;
    }
}
