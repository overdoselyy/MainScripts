using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MIDUI{

    public class StartUIPanel : BaseUIPanel
    {
        //绑定按钮点击事件
        private void Awake()
        {
            Button startBtn = transform.Find("StartBtn").GetComponent<Button>();
            startBtn.onClick.AddListener(() =>
            {
                UIPanelManager.instance.ShowUIPanel("HelpUIPanel");
            });
        }
    }

}
