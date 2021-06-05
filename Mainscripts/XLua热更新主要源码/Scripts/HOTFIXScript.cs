using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;
using UnityEngine.Networking;
public class HOTFIXScript : MonoBehaviour
{

    private LuaEnv luaEnv;
    private Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();
    private void Awake()
    {
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(MyLoader);
        luaEnv.DoString("require 'XLuaTest'");
    }
    private byte[] MyLoader(ref string filePath) {
        string absPath = @"C:\Users\Administrator\Desktop\Lua\" + filePath + ".lua";
        return  System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(absPath));
    }

    private void OnDisable()
    {
        luaEnv.DoString("require 'LuaDispose'");
    }

    private void OnDestroy()
    {
        luaEnv.Dispose();
    }
    [LuaCallCSharp]
    public void LoadResource(string resName, string filePath) 
    {
        StartCoroutine(Load(resName,filePath));
    }
    IEnumerator Load(string resName, string filePath) {
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(@"http://localhost/AssetBundles/" + filePath);
        yield return request.SendWebRequest();
        AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
        GameObject go = ab.LoadAsset<GameObject>(resName);
        prefabs.Add(resName, go);
        print(prefabs[resName]);
    }
    [LuaCallCSharp]
    public GameObject GetPrefab(string resName)
    {
        return prefabs[resName];
    }
}
