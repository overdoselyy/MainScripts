using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 1.ab包相关的api
/// 2.协程
/// 3.字典
/// 4.单例模式
/// 5.委托 =>Lamda表达式
/// </summary>
public class ABManager : SingoAutoMono<ABManager>
{
    //ab包管理器目的让外部方便进行资源加载

    //ab包不能重复加载 会报错
    //用字典存储加载过得ab包
    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();
    //主包
    private AssetBundle mainAB = null;
    //依赖包
    private AssetBundleManifest manifest = null;
    /// <summary>
    /// 这个AB包存放路径 方便修改
    /// </summary>
    private string PathUrl { 
    get{
            return Application.streamingAssetsPath + "/";
        }
    }
    /// <summary>
    /// 主包名 方便修改
    /// </summary>
    private string MainABName {
        get {
#if UNITY_IOS
            return "IOS";
#elif UNITY_ANDROID
            return "Android";
#else 
            return "PC";
#endif
        }
    }
    //加载ab包
    public  void LoadAB(string abName) {
        //获取依赖包相关信息 
        //加载主包 
        //加载主包关键配置文件 
        //加载依赖包
        if (mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        string[] s = manifest.GetAllDependencies(abName);
        AssetBundle ab = null;
        for (int i = 0; i < s.Length; i++)
        {
            //判断包是否加载过
            if (!abDic.ContainsKey(s[i]))
            {
                ab = AssetBundle.LoadFromFile(PathUrl + s[i]);
                abDic.Add(s[i], ab);
            }
        }
        //加载资源来源包
        if (!abDic.ContainsKey(abName))
        {
            ab = AssetBundle.LoadFromFile(PathUrl + abName);
            abDic.Add(abName, ab);
        }
    }

    //同步加载  不指定类型
    public Object LoadRes(string abName, string resName) {
        //加载ab包
        LoadAB(abName);

        //为了外面加载方便 在加载资源时 判断一下是不是Gameobject
        //如果是直接实例化了 返回给外部；
        Object obj = abDic[abName].LoadAsset(resName);
        if (obj is GameObject) {
            return Instantiate(obj);
        }
        else {
            return obj;

        }
    }

    //同步加载 根据Type制定类型
    public Object LoadRes(string abName, string resName, System.Type type) {
        //加载ab包
        LoadAB(abName);

        //为了外面加载方便 在加载资源时 判断一下是不是Gameobject
        //如果是直接实例化了 返回给外部；
        Object obj = abDic[abName].LoadAsset(resName,type);
        if (obj is GameObject)
        {
            return Instantiate(obj);
        }
        else
        {
            return obj;

        }
    }

    //同步加载 根据泛型制定类型
    public T LoadRes<T>(string abName, string resName) where T:Object
    {
        //加载ab包
        LoadAB(abName);

        //为了外面加载方便 在加载资源时 判断一下是不是Gameobject
        //如果是直接实例化了 返回给外部；
        T obj = abDic[abName].LoadAsset<T>(resName);
        if (obj is GameObject)
        {
            return Instantiate(obj);
        }
        else
        {
            return obj;

        }
    }

    //异步加载
    //这里的异步加载 ab并没有使用异步加载
    //只是从ab包中 加载资源时 使用异步

    //根据名字异步加载资源
    public void LoadResAsync(string abName, string resName, UnityAction<Object> callBack) {
        StartCoroutine(RellyLoadResAsync(abName,resName,callBack));
    }
    private IEnumerator RellyLoadResAsync(string abName, string resName, UnityAction<Object> callBack) {
        //加载ab包
        LoadAB(abName);

        //为了外面加载方便 在加载资源时 判断一下是不是Gameobject
        //如果是直接实例化了 返回给外部；
        AssetBundleRequest abr  = abDic[abName].LoadAssetAsync(resName);
        yield return abr;
        //异步加载结束后 通过委托传递给外部
        if (abr.asset is GameObject)
        {
           callBack(Instantiate(abr.asset));
        }
        else
        {
            callBack(Instantiate(abr.asset));

        }
        yield return null;
    }

    //根据Type异步加载资源
    public void LoadResAsync(string abName, string resName, System.Type type, UnityAction<Object> callBack)
    {
        StartCoroutine(RellyLoadResAsync(abName, resName, callBack));
    }
    private IEnumerator RellyLoadResAsync(string abName, string resName,System.Type type,UnityAction<Object> callBack)
    {
        //加载ab包
        LoadAB(abName);

        //为了外面加载方便 在加载资源时 判断一下是不是Gameobject
        //如果是直接实例化了 返回给外部；
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName,type);
        yield return abr;
        //异步加载结束后 通过委托传递给外部
        if (abr.asset is GameObject)
        {
            callBack(Instantiate(abr.asset));
        }
        else
        {
            callBack(Instantiate(abr.asset));

        }
        yield return null;
    }

    //根据泛型异步加载资源
    public void LoadResAsync<T>(string abName, string resName, UnityAction<T> callBack) where T:Object
    {
        StartCoroutine(RellyLoadResAsync<T>(abName, resName, callBack));
    }
    private IEnumerator RellyLoadResAsync<T>(string abName, string resName, UnityAction<T> callBack) where T:Object
    {
        //加载ab包
        LoadAB(abName);

        //为了外面加载方便 在加载资源时 判断一下是不是Gameobject
        //如果是直接实例化了 返回给外部；
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync<T>(resName);
        yield return abr;
        //异步加载结束后 通过委托传递给外部
        if (abr.asset is GameObject)
        {
            callBack(Instantiate(abr.asset)as T);
        }
        else
        {
            callBack(Instantiate(abr.asset)as T);

        }
        yield return null;
    }

    //单个包卸载
    public void UnLoad(string abName) 
    {
        if (abDic.ContainsKey(abName)) {
            abDic[abName].Unload(false);
            abDic.Remove(abName);
        }
    }

    //所有包卸载
    public void ClearAB()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        mainAB = null;
        manifest = null;
    }
}
