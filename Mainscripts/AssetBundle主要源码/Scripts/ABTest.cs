using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABTest : MonoBehaviour
{

    void Start()
    {
        //同步加载测试
        //GameObject obj = ABManager.GetInstance().LoadRes("model", "Cube",typeof(GameObject)) as GameObject;
        //obj.transform.position = Vector3.up;
        //GameObject obj = ABManager.GetInstance().LoadRes("model", "Cube") as GameObject;
        //obj.transform.position = Vector3.up;
        //GameObject obj = ABManager.GetInstance().LoadRes<GameObject>("model", "Cube");
        //obj.transform.position = Vector3.up;

        //GameObject obj2 = ABManager.GetInstance().LoadRes<GameObject>("model", "Cube");
        //obj2.transform.position = Vector3.down;

        //异步加载测试
        //ABManager.GetInstance().LoadResAsync("model", "Cube", (obj) =>
        //{
        //    (obj as GameObject).transform.position = -Vector3.up;
        //});
        ABManager.GetInstance().LoadResAsync<GameObject>("model", "Cube", (obj) =>
        {
            obj.transform.position = -Vector3.up;
        });
        //ABManager.GetInstance().LoadResAsync("model", "Cube",typeof(GameObject) ,(obj) =>
        //{
        //    (obj as GameObject).transform.position = -Vector3.up;
        //});

        //    //加载ab包  不能重复加载
        //    AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "model");
        //    //卸载自己的ab包 参数为true 会把通过ab包加载的资源也删了
        //    //ab.Unload(true);
        //    //加载ab包资源
        //    //GameObject obj = ab.LoadAsset<GameObject>("Cube");
        //    GameObject obj = ab.LoadAsset("Cube",typeof(GameObject)) as GameObject;
        //    Instantiate(obj);

        //    //利用主包获取依赖信息
        //    //加载主包
        //    AssetBundle abMain = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "PC");
        //    //加载主包固定文件
        //    AssetBundleManifest abManifest = abMain.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //    //获取依赖信息
        //    string[] s = abManifest.GetAllDependencies("model");
        //    //得到 依赖包的名字
        //    for (int i = 0; i < s.Length; i++)
        //    {
        //        AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + s[i]);
        //    }

        //    //异步加载
        //   // StartCoroutine(LoadABRes("model", "Cube"));

        //    //卸载所有加载的ab包 参数为true 会把通过ab包加载的资源也删了
        //    //AssetBundle.UnloadAllAssetBundles(false); 

        //}

        //IEnumerator LoadABRes(string ABName, string ResName) {
        //    //加载ab包
        //    AssetBundleCreateRequest abcr =  AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + ABName);
        //    yield return abcr;

        //    //加载资源
        //   AssetBundleRequest abq = abcr.assetBundle.LoadAssetAsync<GameObject>(ResName);
        //    yield return abq;
        //    Instantiate(abq.asset as GameObject);
        //}
        //// Update is called once per frame
        //void Update()
        //{

        //}
    }
}
