using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class TileManager : MonoBehaviour
{
    string file = "Assets/TextFiles/MapTextFile.txt";
    
    public Tilemap tileMap;
    //Tiles to be spawned
    public TileBase tileBase;
    public TileBase tileBaseTwo;
    public TileBase tileBaseThree;
    public TileBase tileBaseFour;

    int rowIndex;
    int columnIndex;

    public int columnLength;
    public int rowLength;

    public Vector2 gridOrigin = Vector2.zero;  // Bottom-left corner of the grid
    public float cellSize = 1f;   


    void Start()
    {
        GenerateMap(file, 10, '#', 20, '~');

        ConvertMapToTileMap(file);                
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
    

    void GenerateMap(string filePath, int height, char c1, int width, char c2)
    {
        using StreamWriter writer = new StreamWriter(filePath, false);
        {
            //Assigning variables to the specific character given in the parameters
            char borderChar = c1;
            char insideChar = c2;
            
            //Looping through the height integer
            for (int y = 0; y < height; y++)
            {
                int rockPosition = UnityEngine.Random.Range(0, width);
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
                
                //Writing each character (column) of each row (except for the top and bottom row and sides) with the character used for the inside
                for (int x = 1; x < width - 1; x++)
                {
                    //if the x position in the rock position AND its not on the border, draw a ROCK
                    if (x == rockPosition && y != 0 && y != height - 1)
                    {
                        writer.Write("$");
                    }                        
                    else
                    {
                        writer.Write(insideChar);
                    }
                                                                               
                }
                
                //Tracking how many characters are in each line
                
                //Writing the Right Border
                writer.Write(borderChar);                                
                
                //Starting a new Line
                writer.WriteLine();
            }
        }        
        writer.Close();        
    }
    void ConvertMapToTileMap(string mapData)
    {
        
        
        Vector3Int offset = new Vector3Int(-10, -5, 0);
        int index = 0;
        string[] mapRows = File.ReadAllLines(mapData);

        int mapWidth = mapRows[0].Length;
        int mapHeight = mapRows.Count();

        

        Debug.Log($"Width: {mapWidth} Height: {mapHeight}");
        
        //Looping through each row
        for (int y = 0; y < mapHeight; y++)
        {
            //Vector3Int rockPosition = new Vector3Int(UnityEngine.Random.Range(0, mapWidth), 0, 0);
            
            //Looping through each coloumn of each row
            for (int x = 0; x < mapWidth; x++)
            {
                char character = mapRows[y][x];
                index += 1;
                Debug.Log(character);

                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                
                //Spawning the tiles
                if (character == '#')
                {
                    ReplaceTile(tilePosition + offset, tileBase);
                }
                else if(character == '~')
                {
                    ReplaceTile(tilePosition + offset, tileBaseTwo);
                }
                else if (character == '$')
                {                    
                    ReplaceTile(tilePosition + offset, tileBaseThree);
                }
                else if (character == '@')
                {
                    ReplaceTile(tilePosition + offset, tileBaseFour);
                }

            }
        }
        Debug.Log(index);
        
        
        //foreach(string s in mapRows)
        //{
        //    Debug.Log($"String: {s} Length: {s.Length}");
        //    int characterCount = s.Length;
            
        //    for (int i = 0; i < characterCount; i++)
        //    {
        //        Debug.Log(i);
        //        char character  = mapRows[]
        //    }

        //}            
                                
    }
}
