using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 抽屉数据 池子中的一列容器
/// </summary>
public class PoolData {
    //对象挂载的父对象
    public GameObject fatherObj;
    //对象的容器
    public List<GameObject> poolList;

    public PoolData(GameObject obj, GameObject poolObj)
    {
        //给抽屉 创建一个父对象 并且把它作为pool对象的子物体
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.parent = poolObj.transform;
        poolList = new List<GameObject>() {};
        PushObj(obj);
    }
    /// <summary>
    /// 往抽屉里面压东西 存起来
    /// </summary>
    /// <param name="obj"></param>
    public void PushObj(GameObject obj)
    {
        //存起来
        poolList.Add(obj);
        //设置父对象
        obj.transform.parent = fatherObj.transform;
        //失活 让其隐藏
        obj.SetActive(false);
    }
    /// <summary>
    /// 从抽屉里面取东西
    /// </summary>
    /// <returns></returns>
    public GameObject GetObj()
    {
        GameObject obj = null;
        //取第一个
        obj = poolList[0];
        poolList.RemoveAt(0);
        //激活
        obj.SetActive(true);
        //断了父子关系
        obj.transform.parent = null;
          
        return obj;
    }
}
    /// <summary>
    /// 缓存池模块
    /// </summary>
    public class PoolMgr : BaseManager<PoolMgr>
{
    //缓存池容器
    Dictionary<string, PoolData> poolDic = new Dictionary<string,PoolData>();
    //父对象
    private GameObject pool;
    /// <summary>
    /// 往外拿东西
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetObj(string name) {
        GameObject obj = null;
        //有抽屉并且抽屉里有东西
        if ( poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0 )
        {
            obj = poolDic[name].GetObj();
        }
        else 
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
            obj.name = name;
        }
        return obj;
    }
   /// <summary>
   /// 还暂时不用的东西
   /// </summary>
   /// <param name="name"></param>
   /// <param name="obj"></param>
    public void PushObj(string name,GameObject obj) {

        if (pool == null)
            pool = new GameObject("Pool");
        //里面有抽屉
        if (poolDic.ContainsKey(name))
        {
            poolDic[name].PushObj(obj);
        }
        //里面没有抽屉
        else {
            poolDic.Add(name,new PoolData(obj,pool));
        }
    }
    /// <summary>
    /// 清空缓存池
    /// </summary>
    public void Clear() {
        poolDic.Clear();
        pool = null;
    }
}
