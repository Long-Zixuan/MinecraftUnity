using System;
using UnityEngine;

public static class Chunk
{
    //遍历一个Chunk中的所有Block
    public static void LoopThroughTheBlocks(ChunkData chunkData, Action<int, int, int> actionToPerform)
    {
        for (int index = 0; index < chunkData.blocks.Length; index++)
        {
            var position = GetPostitionFromIndex(chunkData, index);
            actionToPerform(position.x, position.y, position.z);//对方块进行的改变
        }
    }

    //一个根据block数组的index可以计算出相应block的(x,y,z)位置的算法(来源于StackOverFlow)
		/*      int zDirection=i%zLength;
			int yDirection=(i/zLength)%yLength;
			int xDirection=i/(ylength*zLength);
                */
    //根据Block的位置得到在Block数组中的位置
    private static Vector3Int GetPostitionFromIndex(ChunkData chunkData, int index)
    {
        int x = index % chunkData.chunkSize;
        int y = (index / chunkData.chunkSize) % chunkData.chunkHeight;
        int z = index / (chunkData.chunkSize * chunkData.chunkHeight);
        return new Vector3Int(x, y, z);
    }

    //在Chunk坐标系下
    //判断Block是否超出所属Chunk的界限
    private static bool InRange(ChunkData chunkData, int axisCoordinate)
    {
        if (axisCoordinate < 0 || axisCoordinate >= chunkData.chunkSize)
            return false;

        return true;
    }

    //同上
    private static bool InRangeHeight(ChunkData chunkData, int ycoordinate)
    {
        if (ycoordinate < 0 || ycoordinate >= chunkData.chunkHeight)
            return false;

        return true;
    }
    //根据Chunk坐标系的坐标得出该Block的类型
    public static BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, Vector3Int chunkCoordinates)
    {
        return GetBlockFromChunkCoordinates(chunkData, chunkCoordinates.x, chunkCoordinates.y,chunkCoordinates.z);
    }
  
    public static BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, int x, int y, int z)
    {
        if (InRange(chunkData, x) && InRangeHeight(chunkData, y) && InRange(chunkData, z))
        {
            int index = GetIndexFromPosition(chunkData, x, y, z);
            return chunkData.blocks[index];
        }

        return chunkData.worldReference.GetBlockFromChunkCoordinates(chunkData, chunkData.worldPosition.x + x, chunkData.worldPosition.y + y, chunkData.worldPosition.z + z);
    }

    public static BlockType GetBlockTypeWithLocalPos(ChunkData chunkData, Vector3Int localPosition)
    {
        int index = GetIndexFromPosition(chunkData, localPosition.x, localPosition.y, localPosition.z);
        if (InRange(chunkData, localPosition.x) && InRangeHeight(chunkData, localPosition.y) && InRange(chunkData, localPosition.z))
        {
            return chunkData.blocks[index];
        }
        return chunkData.worldReference.GetBlockFromChunkCoordinates(chunkData, 
            chunkData.worldPosition.x + localPosition.x, chunkData.worldPosition.y + localPosition.y, 
            chunkData.worldPosition.z + localPosition.z);
    }
    //设置Block的类型
    public static void SetBlock(ChunkData chunkData, Vector3Int localPosition, BlockType block)
    {
        if (InRange(chunkData, localPosition.x) && InRangeHeight(chunkData, localPosition.y) && InRange(chunkData, localPosition.z))
        {
            int index = GetIndexFromPosition(chunkData, localPosition.x, localPosition.y, localPosition.z);
            chunkData.blocks[index] = block;
        }
        else
        {
            throw new Exception("Need to ask World for appropiate chunk");
        }
    }
    //从一个Block的坐标得到在数组中的位置
    /*i (x,y,z)
			0 (0,0,0)
			1 (0,0,1)
			2 (0,0,2)
			3 (0,1,0)
			4 (0,1,1)
		  5 (0,1,2)
			6 (0,2,0)
			......
			19 (2,0,1)
			26 (2,2,2)
			i=z+y*ZSize+x*ZSize*YSize*/
    private static int GetIndexFromPosition(ChunkData chunkData, int x, int y, int z)
    {
        return x + chunkData.chunkSize * y + chunkData.chunkSize * chunkData.chunkHeight * z;
    }
    
    private static int GetIndexFromPosition(ChunkData chunkData, Vector3Int pos)
    {
        return pos.x + chunkData.chunkSize * pos.y + chunkData.chunkSize * chunkData.chunkHeight * pos.z;
    }
    //根据Chunk坐标系的坐标得到对应的Block
    public static Vector3Int GetBlockInChunkCoordinates(ChunkData chunkData, Vector3Int pos)
    {
        return new Vector3Int
        {
            x = pos.x - chunkData.worldPosition.x,
            y = pos.y - chunkData.worldPosition.y,
            z = pos.z - chunkData.worldPosition.z
        };
    }
    //获得MeshData的方法
    public static MeshData GetChunkMeshData(ChunkData chunkData)
    {
        MeshData meshData = new MeshData(true);

        LoopThroughTheBlocks(chunkData, (x, y, z) => meshData = BlockHelper.GetMeshData(chunkData, x, y, z, meshData, chunkData.blocks[GetIndexFromPosition(chunkData, x, y, z)]));


        return meshData;
    }
    
    internal static Vector3Int ChunkPositionFromBlockCoords(World world, int x, int y, int z)
    {
        Vector3Int pos = new Vector3Int
        {
            x = Mathf.FloorToInt(x / (float)world.chunkSize) * world.chunkSize,
            y = Mathf.FloorToInt(y / (float)world.chunkHeight) * world.chunkHeight,
            z = Mathf.FloorToInt(z / (float)world.chunkSize) * world.chunkSize
        };
        return pos;
    }
}