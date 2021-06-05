using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;


namespace MIDUI {
    public class UIPanelManager 
    {
        //面板父亲
        Transform uiRoot;

        public Transform UIRoot {
            get {
                if (uiRoot == null) {

                    uiRoot = GameObject.FindWithTag("UIRoot").transform;
                    GameObject.DontDestroyOnLoad(uiRoot);
                }
                return uiRoot;
            }
        
        }

        //面板对象容器   缓存
        Dictionary<string, BaseUIPanel> uiPanelsDic = new Dictionary<string, BaseUIPanel>();

        //预制体容器   资源
        Dictionary<string, BaseUIPanel> prefabsDic = new Dictionary<string, BaseUIPanel>();

        //加载xml文件 加载预制体资源
        public void Init(string fileName) {
            //读取根标签
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            var uiRoot = doc.SelectSingleNode("UIRoot") as XmlElement;
            //读取子标签
            foreach (XmlElement panel in uiRoot.ChildNodes)
            {
                string name = panel.GetAttribute("name");
                string prefabPath = panel.GetAttribute("prefabPath");

                BaseUIPanel prefab = Resources.Load<BaseUIPanel>(prefabPath);
                prefabsDic.Add(name, prefab);//放入容器管理
            }

        }
        //开启指定面板
        public void ShowUIPanel(string uiPanelName)
        {
            //判断对象是否有缓存
            if (uiPanelsDic.ContainsKey(uiPanelName)) {

                uiPanelsDic[uiPanelName].Show();//显示面板
                return;

            }
            //从预制体容器创建
            if (prefabsDic.ContainsKey(uiPanelName)) //判断预设是否存在
            {
                var uiPanel = GameObject.Instantiate<BaseUIPanel>(prefabsDic[uiPanelName]);
                uiPanel.transform.parent = UIRoot;//设置父亲
                uiPanel.Show();
                //加入到缓存器
                uiPanelsDic.Add(uiPanelName, uiPanel);
            }
        }

        //关闭指定面板
        public void HideUIPanel(string uiPanelName) {
            if (uiPanelsDic.ContainsKey(uiPanelName))
            {

                uiPanelsDic[uiPanelName].Hide();//显示面板
                return;

            }
        }
        //关闭所有面板
        public void HideAllUIPanel() {
            foreach (var panel in uiPanelsDic.Values)
            {
                panel.Hide();
            }
        }
        //构造函数
        UIPanelManager() {
            Init(AppConst.uiPrefabXML);
        
        }
        //单例模式
        public static UIPanelManager instance = new UIPanelManager();

    }
}
