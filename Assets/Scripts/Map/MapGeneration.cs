using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGeneration : MonoBehaviour
{
    public GameObject Town;
    public Tile defaultTile;
    public Tile groundTile;
    public Tile forestTile;
    public Tile waterTile;
    public Tile mountainTile;
    public Tile prohibitTile;

    public Tile pathTile;

    public Tilemap groundLayer;
    public Tilemap forestLayer;
    public Tilemap waterLayer;
    public Tilemap mountLayer;

    private string[,] maploc;
    public List<Vector2> objloc;
    private float[][] perlinNoise;

    // Use this for initialization
    void Start()
    {
        objloc = GameObject.FindGameObjectWithTag("creator").GetComponent<MapCreator>().objloc;
        maploc = GameObject.FindGameObjectWithTag("creator").GetComponent<MapCreator>().maploc;
        // map = MapCreator.Instance.map;
        initialMap();
    }

    private void initialMap()
    {
        //tileMap
        for (int i = 0;i<maploc.GetLength(0);i++)
        {
            for (int j = 0; j < maploc.GetLength(1); j++)
            {
                //平地
                groundLayer.SetTile(new Vector3Int(i, j, 0), groundTile);
                //森林
                if (maploc[i, j].Equals("f"))
                {
                    forestLayer.SetTile(new Vector3Int(i, j, 0), forestTile);
                }
                else if (maploc[i, j].Equals("w"))
                {
                    waterLayer.SetTile(new Vector3Int(i, j, 0), waterTile);
                }
                else if (maploc[i, j].Equals("p"))
                {
                    groundLayer.SetTile(new Vector3Int(i, j, 0), pathTile);
                }
            }
            
        }
        //objList
        //城镇
        foreach (Vector2 v in objloc)
        {
            GameObject.Instantiate(Town, v, Quaternion.identity, null);
        }
    }

    //private void initialMap()
    //{
    //    for (int i = 0; i < map.GetLength(0); i++)
    //    {
    //        for (int j = 0; j < map.GetLength(1); j++)
    //        {
    //            groundLayer.SetTile(new Vector3Int(i, j, 0), groundTile);

    //            if (map[i, j] == MapCreator.TileType.Forest) {
    //                forestLayer.SetTile(new Vector3Int(i, j, 0), forestTile);
    //            }
    //        }
    //    }
    //}
}

