using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileChanger : MonoBehaviour
{

    public Tilemap wallTileMap;
    public Tilemap floorTileMap;

    public TileBase floor;
    public TileBase wall;

    public Vector3Int TilePos1;
    public Vector3Int TilePos2;

    //Changes wall at a specific point to floor using tiles
    public void WallToFloor()
    {
        wallTileMap.SetTile(TilePos1, null);
        wallTileMap.SetTile(TilePos2, null);
        floorTileMap.SetTile(TilePos1, floor);
        floorTileMap.SetTile(TilePos2, floor);
    }

    //Changes floor at a specific point to wall using tiles
    public void FloorToWall()
    {
        floorTileMap.SetTile(TilePos1, null);
        floorTileMap.SetTile(TilePos2, null);
        wallTileMap.SetTile(TilePos1, wall);
        wallTileMap.SetTile(TilePos2, wall);
    }







}
