using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//决定每个Block的纹理等信息
[CreateAssetMenu(fileName ="Block Data" ,menuName ="Data/Block Data")]
public class BlockDataSO : ScriptableObject
{
    public float textureSizeX, textureSizeY;
    public List<TextureData> textureDataList;
}

[Serializable]
public class TextureData
{
    public BlockType blockType;//是什么类型的方块
    public Vector2Int up, down, side;//up为Block上面的贴图side为旁边的

    //为什么是Vector2类型？-因为我们在Chunk预制体重保存了贴图信息(MainTexture和WaterTexture)
    //MainTexture是一张包含很多贴图的综合，我们需要通过UV坐标来映射正确的贴图
    //所以Vector2其实就是UV坐标来定位具体的贴图，例如Grass_Dirt(Block类型)
    //其up坐标是6,8对应到MainTexture上就是草，(默认四个Side面的texture都一样)Side就对应的Grass_Dirt

    public bool isSolid = true;//是否是固体，水就为false
    public bool generatesCollider = true;//是否生成碰撞体
}