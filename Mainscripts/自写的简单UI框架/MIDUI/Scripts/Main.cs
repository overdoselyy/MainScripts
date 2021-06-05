using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MIDUI {

    public class Main : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(GameObject.Find("EventSystem"));
            DontDestroyOnLoad(this);
            UIPanelManager.instance.ShowUIPanel("MainPanel");
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Y)) {

                SceneManager.LoadScene(1);
            
            }
        }
    }

}

