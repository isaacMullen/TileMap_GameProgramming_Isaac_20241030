using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tilemap tileMap;
    public TileBase tileBase;

    int rowIndex;
    int columnIndex;

    public int columnLength;
    public int rowLength;

    public Vector2 gridOrigin = Vector2.zero;  // Bottom-left corner of the grid
    public float cellSize = 1f;

    // Start is called before the first frame update
    void Start()
    {
        columnIndex = 12;
        rowIndex = 4;
        for (int i = 0; i < columnLength; i++)
        {
            //Draw the left side
            ReplaceTile(new Vector3Int(-columnIndex, rowIndex, 0), tileBase);

            //Drawing Top border
            for (int t = -rowLength; t < rowLength; t++)
            {
                if (rowIndex == 4)
                {
                    ReplaceTile(new Vector3Int(t, 4, 0), tileBase);
                }                
            }

            //Draw Bottom border
            for (int t = -rowLength; t < rowLength; t++)
            {
                if (rowIndex == -5)
                {
                    ReplaceTile(new Vector3Int(t, -5, 0), tileBase);
                }                
            }
            
            
            //Draw the right side
            ReplaceTile(new Vector3Int(columnIndex, rowIndex, 0), tileBase);            
            rowIndex--;

            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 gridPosition = mouseWorldPos - (Vector3)gridOrigin;

        int column = Mathf.FloorToInt(gridPosition.x / cellSize);
        int row = Mathf.FloorToInt(gridPosition.y / cellSize);

        Debug.Log($"Column: {column}, Row: {row}");
    }

    void ReplaceTile(Vector3Int tileToReplace, TileBase tile)
    {
        tileMap.SetTile(tileToReplace, tile);
    }
}
