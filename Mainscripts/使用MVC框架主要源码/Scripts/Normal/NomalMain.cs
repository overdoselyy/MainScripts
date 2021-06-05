using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            //显示主面板
            MainPanel.ShowMe();
        }
        else if (Input.GetKeyDown(KeyCode.M)) {
            //隐藏主面板
            MainPanel.HideMe();
        }
    }
}
