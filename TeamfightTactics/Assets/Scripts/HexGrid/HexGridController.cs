using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridController : MonoBehaviour
{
    [Header("Grid Settings")]
    [field: SerializeField]
    private Vector2Int gridSize;
    [field: SerializeField]
    private string tileName = "HexTile ";
    [field: SerializeField]
    private Vector3 gridPosition;
    [field: SerializeField]
    private Vector3 gridRotation;
    [field: SerializeField]
    private int tileLayer;

    [Header("Tile Settings")]
    [field: SerializeField]
    private Material material;  
    [field: SerializeField]
    private float innerSize = 0f;
    [field: SerializeField]
    private float outerSize = 1.0f;
    [field: SerializeField]
    private float height = 1.4f;

    private void Awake()
    {
        CreateGridLayer();
        this.transform.position = gridPosition;
        this.transform.Rotate(gridRotation);
        this.transform.localScale = new Vector3(0.6f, 1f, 0.6f);
    }

    private void CreateGridLayer()
    {
        for(int y=0; y < gridSize.y; ++y)
        {
            for(int x=0; x < gridSize.x; ++x)
            {
                GameObject tile = new GameObject(tileName+$"{x},{y}",typeof(HexRenderer));
                tile.transform.position = GetPositionForHexFromCoordinate(new Vector2Int(x,y));
                tile.layer = 3;

                HexRenderer hexRenderer = tile.GetComponent<HexRenderer>();
                hexRenderer.outerSize = outerSize;
                hexRenderer.innerSize = innerSize;
                hexRenderer.height = height;
                hexRenderer.material = material;
                hexRenderer.DrawMesh();

                tile.transform.SetParent(transform, true);
            }
        }
    }

    private Vector3 GetPositionForHexFromCoordinate(Vector2Int coordinate)
    {
        int column = coordinate.x;
        int row = coordinate.y;

        float width;
        float height;
        float xPosition;
        float yPosition;
        bool shouldOffset;
        float horizontalDistance;
        float verticalDistance;
        float offset;
        float size = outerSize;
        
        shouldOffset = (row % 2) == 0;
        width = Mathf.Sqrt(3) * size;
        height = 2f * size;

        horizontalDistance = width;
        verticalDistance = height * (3f/4f);

        offset = (shouldOffset) ? width/2 : 0;

        xPosition = (column * (horizontalDistance)) + offset;
        yPosition = (row * verticalDistance);

        return new Vector3(xPosition, 0, -yPosition);
    }
}
