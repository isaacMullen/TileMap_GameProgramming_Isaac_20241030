using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    string file = "Assets/TextFiles/MapTextFile.txt";
    
    public Tilemap tileMap;
    public TileBase tileBase;

    int rowIndex;
    int columnIndex;

    public int columnLength;
    public int rowLength;

    public Vector2 gridOrigin = Vector2.zero;  // Bottom-left corner of the grid
    public float cellSize = 1f;   


    void Start()
    {
        GenerateMap(10, '#', 10, '~', file);

        columnIndex = 12;
        rowIndex = 4;
        for (int i = 0; i < columnLength; i++)
        {
            //Draw the left side
            ReplaceTile(new Vector3Int(-columnIndex, rowIndex, 0), tileBase);

            //Drawing Top border
            for (int t = -rowLength; t < rowLength; t++)
            {
                //Top border Y position
                if (rowIndex == 4)
                {
                    ReplaceTile(new Vector3Int(t, 4, 0), tileBase);
                }   
                else if (rowIndex == -5)
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

        //Debug.Log($"Column: {column}, Row: {row}");
    }

    void ReplaceTile(Vector3Int tileToReplace, TileBase tile)
    {
        tileMap.SetTile(tileToReplace, tile);
    }
    

    void GenerateMap(int height, char c1, int width, char c2, string filePath)
    {
        using StreamWriter writer = new StreamWriter(filePath, false);
        {
            //Assigning variables to the specific character given in the parameters
            char borderChar = c1;
            char insideChar = c2;
            
            //Looping through the height integer
            for (int y = 0; y < height; y++)
            {
                //Checking if we are at the top or bottom of the screen (height variable)
                if (y == 0 || y == height - 1)
                {
                    //Assigning the character to be drawn between both sides, to the border character
                    insideChar = c1;
                }
                //Otherwise, the inside character is reset to its original value
                else insideChar = c2;
                
                //Writing the Left Border
                writer.Write(borderChar);
                
                //Writing each column of each row (except for the top and bottom row) with the character used for the inside
                for (int x = 0; x < width; x++)
                {                    
                    writer.Write(insideChar);
                }
                
                //Writing the Right Border
                writer.Write(borderChar);


                writer.WriteLine();
            }
        }
        writer.Close();
        
    }
}
