using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 生成弹痕
/// </summary>
public class Bulletmark : MonoBehaviour
{
    // <summary>
    /// 弹痕图片
    /// </summary>
    public Texture2D danhen;
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            //射线信息的载体
            RaycastHit hit;
            //创建射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //发射射线
            if (Physics.Raycast(ray, out hit))
            {
                //生成弹痕
                Instantiate(danhen, hit.point + new Vector3(0, 0, -0.1f), Quaternion.LookRotation(hit.normal));
                //辅助划线
                Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
                Debug.DrawRay(hit.point, hit.normal, Color.green);

            }
        }
    }
}
     

