﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance => instance;

    public System.Action LevelCompleted;

    [Space]
    [SerializeField]
    LevelInfoAsset levelInfoAsset;

    [Space]
    [SerializeField]
    private Transform blockContainer;

    [Space]
    [SerializeField]
    private Transform start;

    private static LevelManager instance;

    int currentLevelIndex = 0;

    BlockSpawner blockSpawner = new BlockSpawner();

    List<BlockController> createdBlocks = new List<BlockController>();
    List<BlockController> collectedBlocks = new List<BlockController>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        blockSpawner = GetComponent<BlockSpawner>();
    }
    public void Update()
    {
        HandleCurrentLevel();
    }
    public bool HandleCurrentLevel()
    {
        if (BlockSpawner.filledCubes < BlockSpawner.totalSize)
        {
            Debug.Log($"{BlockSpawner.filledCubes} / { BlockSpawner.totalSize} <- Collected Block Count");
            return false;
        }
        else
            Debug.Log("Level Finished.");
        return true;

        
    }

    public bool HandleCreateNextLevel()
    {
        if(createdBlocks.Count > 0)
        {
            for (int i = 0; i < createdBlocks.Count; i++)
            {
                Destroy(createdBlocks[i]);
            }
        }

        ++currentLevelIndex;

        if (levelInfoAsset.levelInfos.Count >= currentLevelIndex)
        {
            CreateNextLevel();
            return true;
        }

        return false;
    }

    void CreateNextLevel()
    {
        blockSpawner.CreateBlockFromImage(levelInfoAsset.levelInfos[currentLevelIndex - 1], blockContainer);
        blockSpawner.CreateFillBlock(levelInfoAsset.levelInfos[currentLevelIndex - 1], blockContainer,start);
    }

    public void OnBlockCreated(BlockController blockController)
    {
        createdBlocks.Add(blockController);
        //Debug.Log("Collected Block Count " + collectedBlocks.Count);
    }

    public void OnBlockCollected(BlockController blockController)
    {
        collectedBlocks.Add(blockController);
        //Debug.Log($"{collectedBlocks.Count} / {createdBlocks.Count} <- Collected Block Count");

        if (collectedBlocks.Count == createdBlocks.Count)
        {
            LevelCompleted?.Invoke();
        }
    }
}
