using MIDUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class Main : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //启动UI 创建面板
            UIPanelManager.instance.ShowUIPanel("StartUIPanel");
        //启动MVC框架
        AppFacade.instance.StartUp();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }


