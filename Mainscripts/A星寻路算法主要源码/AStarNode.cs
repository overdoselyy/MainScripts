using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 格子类型
/// </summary>
public enum Type { 
    Walk,
    Stop,
}
/// <summary>
/// AStar格子类
/// </summary>
public class AStarNode
{
    //格子的坐标
    public int x;
    public int y;
    //寻路消耗
    public float f;
    //到起点距离
    public float g;
    //到终点的距离
    public float h;
    //父对象
    public AStarNode father;
    //格子类型
    public Type type;
    /// <summary>
    /// 构造函数，传入坐标和格子类型
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="type"></param>
    public AStarNode(int x,int y,Type type) {
        this.x = x;
        this.y = y;
        this.type = type;
    }
}
