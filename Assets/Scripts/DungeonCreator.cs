using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCreator : MonoBehaviour
{
    [SerializeField] public int roomSize;
    [SerializeField] public int corridorWidth;
    [SerializeField] public int maxMainIterations;
    // Start is called before the first frame update
    void Start()
    {
        CreateDungeon();
    }

    private void CreateDungeon()
    {
        DungeonGenerator generator = new DungeonGenerator(roomSize, corridorWidth, maxMainIterations);
        generator.startCreate();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

}
