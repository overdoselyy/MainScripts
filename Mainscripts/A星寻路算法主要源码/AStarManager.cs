using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A星寻路管理器单例模式
/// </summary>
public class AStarManager
{
    private static AStarManager instance;
    public static AStarManager Instance 
    {
        get 
        {
            if (instance == null) {
                instance = new AStarManager();
            }
            return instance;
        }
    }
    //地图宽高
    private int mapW;
    private int mapH;
    //地图相关的所有格子对象容器
    public AStarNode[,] nodes;
    //开启列表
    private List<AStarNode> openList =new List<AStarNode>();
    //关闭列表
    private List<AStarNode> closeList = new List<AStarNode>();
    /// <summary>
    /// 初始化地图
    /// </summary>
    /// <param name="w"></param>
    /// <param name="h"></param>
    public void InitMapInfo(int w, int h) {
        this.mapH = h;
        this.mapW = w;
        //根据宽高创造格子
        nodes = new AStarNode[w, h]; 
        for (int i = 0; i < w; i++) {
            for (int j = 0; j < h; j++) {
                AStarNode node = new AStarNode(i, j, Random.Range(0, 100) < 20 ? Type.Stop : Type.Walk);
                nodes[i, j] = node;
            }
        }
    }
    /// <summary>
    /// 寻路方法
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    /// <returns></returns>
    public List<AStarNode> FindPath(Vector2 startPos, Vector2 endPos) {
        //判断传入的两个点是否合法
        //1.首先在地图范围内
        //2.不是阻挡
        if (startPos.x > mapW || startPos.y > mapH || 
            endPos.x > mapW || endPos.y > mapH || 
            startPos.x < 0 || startPos.y < 0 ||
            endPos.x <0 || endPos.y<0) {
            return null;
        }
        //得到起点和终点对应的格子
        AStarNode endNode = nodes[(int)endPos.x,(int)endPos.y];
        AStarNode startNode = nodes[(int)startPos.x,(int)startPos.y];
        if (startNode.type == Type.Stop || endNode.type == Type.Stop) {
            return null;
        }
        //清空上一次的信息
        openList.Clear();
        closeList.Clear();
        //把开始点放入关闭列表中
        startNode.father = null;
        startNode.f = 0;
        startNode.g = 0;
        startNode.h = 0;
        closeList.Add(startNode);

        while (true) 
        {
            //从起点开始找周围的点放入开启列表
            //左上
            FindNearlyToOpenList(startNode.x - 1, startNode.y - 1, 1.4f, startNode, endNode);
            //左下
            FindNearlyToOpenList(startNode.x - 1, startNode.y + 1, 1.4f, startNode, endNode);
            //左
            FindNearlyToOpenList(startNode.x - 1, startNode.y, 1, startNode, endNode);
            //右
            FindNearlyToOpenList(startNode.x + 1, startNode.y, 1, startNode, endNode);
            //右上
            FindNearlyToOpenList(startNode.x + 1, startNode.y - 1, 1.4f, startNode, endNode);
            //右下
            FindNearlyToOpenList(startNode.x + 1, startNode.y + 1, 1.4f, startNode, endNode);
            //上
            FindNearlyToOpenList(startNode.x, startNode.y - 1, 1, startNode, endNode);
            //下
            FindNearlyToOpenList(startNode.x, startNode.y + 1, 1, startNode, endNode);

            //死路判断
            if (openList.Count == 0) {
                Debug.Log("死路");
                return null;
            }

            //选出开启列表中寻路消耗最小的点放入关闭列表并从开启列表删除
            openList.Sort(SortOpenList);
            closeList.Add(openList[0]);
            //找到这个点作为下次寻路开始的点
            startNode = openList[0];
            openList.RemoveAt(0);

            //如果这个点是终点，得到最终结果返回，不是则继续寻路
            if (startNode == endNode)
            {
                List<AStarNode> path = new List<AStarNode>();
                path.Add(endNode);
                while (endNode.father != null){
                    endNode = endNode.father;
                    path.Add(endNode);
                }
                //列表翻转的API
                path.Reverse();
                return path;
            }
        }

    }
    /// <summary>
    /// 对两点进行比较排序
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private int SortOpenList(AStarNode a, AStarNode b) {
        if (a.f > b.f)
        {
            return 1;
        }
        else {
            return -1;
        }
    }
    /// <summary>
    /// 把邻近的点放入开启列表中
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="g"></param>
    /// <param name="father"></param>
    /// <param name="endNode"></param>
    private void FindNearlyToOpenList(int x, int y,float g, AStarNode father, AStarNode endNode) {

        //判断这些点是否是边界，阻挡，是否在开启或关闭列表，都不是才放入
            if (x >= mapW || y >= mapH ||
                x < 0 || y < 0){
                return;
            }
            AStarNode node = nodes[x, y];
            if (node == null ||
                node.type == Type.Stop ||
                openList.Contains(node) ||
                closeList.Contains(node))
            {
                return;
            }
            //计算父对象
            node.father = father;
            node.g = father.g+g;
            node.h = Mathf.Abs(node.x - endNode.x) + Mathf.Abs(node.y - endNode.y);
            node.f = node.g + node.h;
            openList.Add(node);

        }
    }
