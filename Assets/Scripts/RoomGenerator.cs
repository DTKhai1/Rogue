using System.Collections.Generic;
using UnityEngine;

internal class RoomGenerator
{
    private int roomSize;
    public RoomGenerator(int roomSize)
    {
        this.roomSize = roomSize;
    }

    public Vector2Int RoomGeneration(HashSet<Vector2Int> Pos)
    {
        int walkLength = roomSize * 2;

    }
}