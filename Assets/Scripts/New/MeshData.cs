using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于保存Face的Mesh数据信息
/// </summary>
public class MeshData
{
    //用List而不用数组是为了更容易添加新的顶点进去
    public List<Vector3> vertices = new List<Vector3>();
    public List<int> triangles = new List<int>();
    public List<Vector2> uv = new List<Vector2>();
    //单独出来是因为有些Face是需要渲染出来而不需要有碰撞，比如Water
    public List<Vector3> colliderVertices = new List<Vector3>();
    public List<int> colliderTriangles = new List<int>();

    public MeshData waterMesh;
    private bool isMainMesh = true;//不是水方块

    //构造函数，是为了确定每个Chunk上的Mesh，isMainMesh是true就执行waterMesh的
    //构造函数，但传入的是false,也就是waterMesh将会是空
    public MeshData(bool isMainMesh)
    {
        if (isMainMesh)
        {
            waterMesh = new MeshData(false);
        }
    }
    //添加顶点
    public void AddVertex(Vector3 vertex, bool vertexGeneratesCollider)
    {
        vertices.Add(vertex);
        if (vertexGeneratesCollider)
        {
            colliderVertices.Add(vertex);
        }

    }
    //添加三角面，一个Mesh实际就是很多个三角面
    public void AddQuadTriangles(bool quadGeneratesCollider)
    {
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 2);

        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 1);

        //如果要生成碰撞，需要将顶点再加入到colliderVertices
        if (quadGeneratesCollider)
        {
            colliderTriangles.Add(colliderVertices.Count - 4);
            colliderTriangles.Add(colliderVertices.Count - 3);
            colliderTriangles.Add(colliderVertices.Count - 2);
            colliderTriangles.Add(colliderVertices.Count - 4);
            colliderTriangles.Add(colliderVertices.Count - 2);
            colliderTriangles.Add(colliderVertices.Count - 1);
        }
    }
}