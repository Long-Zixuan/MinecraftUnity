using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Chunk是很多个Block组成的。
/// 防止一个一个Block的渲染，减少性能消耗。
/// </summary>
public class ChunkData
{
    public BlockType[] blocks;
    public int chunkSize = 16;
    public int chunkHeight = 100;
    public World worldReference;
    public Vector3Int worldPosition;//每一个Chunk都有一个位置

    public ChunkData(int chunkSize, int chunkHeight, World world, Vector3Int worldPosition)
    {
        this.chunkHeight = chunkHeight;
        this.chunkSize = chunkSize;
        this.worldReference = world;
        this.worldPosition = worldPosition;
        blocks = new BlockType[chunkSize * chunkHeight * chunkSize];
    }

}