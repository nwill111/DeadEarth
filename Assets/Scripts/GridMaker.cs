using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GridMaker : MonoBehaviour
{
    public Tilemap walkableTilemap;
    public Vector3Int[,] tiles;
    public GameObject player;
    public GameObject enemy;
    public AStar astar;
    BoundsInt bounds;
    public float maxSpeed;
    Coroutine moveIE;


    public void Start()
    {
        walkableTilemap.CompressBounds();
        bounds = walkableTilemap.cellBounds;

        astar = new AStar(tiles, bounds.size.x, bounds.size.y);
      

    }

    //Creates grid for A* pathfinding
    public void CreateGrid()
    {
        int width = bounds.size.x;
        int height = bounds.size.y;
        tiles = new Vector3Int[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int x = bounds.xMin + i;
                int y = bounds.yMin + j;
                if (walkableTilemap.HasTile(new Vector3Int(x, y, 0)))
                {
                    tiles[i, j] = new Vector3Int(x, y, 0);
                }
                else
                {
                    tiles[i, j] = new Vector3Int(x, y, 1);
                }
            }
        }

    }

    public void Update()
    {
       
            //Update grid for A* pathfinding
            CreateGrid();

            if (moveIE != null)
            {
                StopCoroutine(moveIE);
            }

            Vector3 PlayerPos = player.transform.position;
            Vector3 EnemyPos = enemy.transform.position;

            Vector3Int playerGridPos = walkableTilemap.WorldToCell(PlayerPos);
            Vector3Int enemyGridPos = walkableTilemap.WorldToCell(EnemyPos);
            
            //Get path from A* pathfinding
            var path = astar.FindPath(tiles, playerGridPos, enemyGridPos);

            //Remove the current point from the path (this solves an issue where the enemy would not want to move from its current position)
            path.RemoveAt(0);

            StartCoroutine(MoveAlongPath(path));
        
    }


    //Moves enemy along path
    IEnumerator MoveAlongPath(List<Node> path)
    {
        for (int i = 0; i < path.Count; i++)
        {
            var node = path[i];

            moveIE = StartCoroutine(Move(i, path));
            yield return moveIE;

           
        }

        
    }

    //Moves enemy to a specific point
    IEnumerator Move(int i, List<Node> path)
    {

        var worldPos = walkableTilemap.CellToWorld(new Vector3Int(path[i].x, path[i].y, 0));
        //Off set to center of tile
        worldPos.x += 0.5f;
        worldPos.y += 0.5f;
        worldPos.z = 0;
        while (enemy.transform.position != worldPos)
        {

            // Get the direction the object should move in
            Vector2 direction = (worldPos - enemy.transform.position).normalized;

            // If the object is moving
            if (direction != Vector2.zero)
            {
                // Calculate the angle the object should be rotated to face the direction it is moving
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Smoothly rotate the object towards the calculated angle
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, Quaternion.Euler(0, 0, angle), (5f * Time.deltaTime));
            }

            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, worldPos, (maxSpeed * Time.deltaTime));

            
            yield return null;
        }
        


    }

    
}
