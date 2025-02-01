using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator
{
    private int roomSize;
    private int corridorWidth;
    private int maxMainIterations;


    public HashSet<Vector2Int> dungeonFloor = new HashSet<Vector2Int>();
    public HashSet<Vector2Int> roomPos = new HashSet<Vector2Int>();

    public DungeonGenerator(int roomSize, int corridorWidth, int maxMainIterations)
    {
        this.roomSize = roomSize;
        this.corridorWidth = corridorWidth;
        this.maxMainIterations = maxMainIterations;
    }

    public void startCreate()
    {
        Vector2Int mainDirection = GetRandomDirection();
        RoomGenerator mainGenerator = new RoomGenerator(roomSize);
        dungeonFloor.Add(mainGenerator.RoomGeneration(roomPos));

        GetRoomPos(mainDirection);
    }

    private void GetRoomPos(Vector2Int mainDirection)
    {
        Vector2Int currentPos = Vector2Int.zero;
        roomPos.Add(currentPos);
        for (int i = 0; i < maxMainIterations; i++)
        {
            Vector2Int mainOffSet = mainDirection * Random.Range(60, 80);
            currentPos += mainOffSet;
            roomPos.Add(currentPos);
        }
        GetSidePos();
    }
    
    private void GetSidePos()
    {
        
    }

    private Vector2Int GetRandomDirection()
    {
        List<Vector2Int> directions = new List<Vector2Int>
        {
            Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
        };

        return directions[Random.Range(0, directions.Count)];
    }

}
