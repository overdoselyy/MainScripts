using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MIDUI
{
    public class BaseUIPanel : MonoBehaviour
    {
        Transform closeBtn;
        public Transform CloseBtn {

            get {
                if (closeBtn == null) {
                    closeBtn = transform.Find("CloseBtn");
                }
                return closeBtn;
            
            }
        }
        private void Awake()
        {
            if (CloseBtn != null) {

                CloseBtn.GetComponent<Button>().onClick.AddListener(Hide);
            }

        }

        public virtual void Show()
        {
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.SetActive(true);
        }
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }


}
