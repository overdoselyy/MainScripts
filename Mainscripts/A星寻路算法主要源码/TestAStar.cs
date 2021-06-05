using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAStar : MonoBehaviour
{
    //左上角第一个立方体的位置
    public int beginX = -3;
    public int beginY = 5 ;

    //之后每一个立方体偏移的位置
    public int offsetX = 2;
    public int offsetY = 2;
    //地图格子的宽高
    public int w = 5;
    public int h = 5;

    public Material red;
    public Material yellow;
    public Material green;
    public Material white;
    //开始点，给他一个为负的坐标点
    private Vector2 beginPos = Vector2.right * -1;
    private Dictionary<string, GameObject> cubes = new Dictionary<string, GameObject>();

    List<AStarNode> list;
    // Start is called before the first frame update
    void Start()
    {
        AStarManager.Instance.InitMapInfo(w, h);
        for (int i = 0; i < w; i++) {
            for (int j = 0; j < h; j++) {
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = new Vector3(beginX + i * offsetX, beginY + j * offsetY,0);
                //名字
                obj.name = i + "_" + j;
                //存储立方体到字典容器
                cubes.Add(obj.name, obj);
                AStarNode node = AStarManager.Instance.nodes[i, j];
                if (node.type == Type.Stop) {
                    obj.GetComponent<MeshRenderer>().material = red;
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            //得到屏幕鼠标位置发出去的射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000)) {
                //得到点击到的立方体，才知道第几行第几列

                if (beginPos == Vector2.right * -1)
                {
                    //清理上一次的路径，把绿色变为白色
                    if (list != null)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            cubes[list[i].x + "_" + list[i].y].GetComponent<MeshRenderer>().material = white;
                        }
                    }
                    string[] strs = hit.collider.gameObject.name.Split('_');
                    beginPos = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));
                    hit.collider.gameObject.GetComponent<MeshRenderer>().material = yellow;
                }
                //有起点获得终点
                else {
                    string[] strs = hit.collider.gameObject.name.Split('_');
                    Vector2 endPos = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));
                    //寻路
                    list =  AStarManager.Instance.FindPath(beginPos, endPos);
                    //避免死路导致黄色不清除
                    cubes[(int)beginPos.x + "_" + (int)beginPos.y].GetComponent<MeshRenderer>().material = white;
                    //不为空找成功
                    if (list != null) {
                        for (int i = 0; i < list.Count; i++) {
                            cubes[list[i].x + "_" + list[i].y].GetComponent<MeshRenderer>().material = green;
                        }
                    }
                    
                    //清除开始点，变成初始值
                    beginPos = Vector2.right * -1;
                }

            }
        }
        
    }
}
