using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.IO;
public class LoadLua : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Load());
    }

    IEnumerator Load() {
        UnityWebRequest request = UnityWebRequest.Get(@"http://localhost/" + "XLuaTest.lua");
        yield return request.SendWebRequest();
        string str = request.downloadHandler.text;
        File.WriteAllText(@"C:\Users\Administrator\Desktop\Lua\XLuaTest.lua", str);
        UnityWebRequest request1 = UnityWebRequest.Get(@"http://localhost/" + "LuaDispose.lua");
        yield return request1.SendWebRequest();
        string str1 = request1.downloadHandler.text;
        File.WriteAllText(@"C:\Users\Administrator\Desktop\Lua\LuaDispose.lua", str1);
        SceneManager.LoadScene(1);
    }
}
