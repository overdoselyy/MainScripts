using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 开启 关闭垂直同步
/// </summary>
public class FpsBtn : MonoBehaviour
{
    public void VsyncOpen() {
        QualitySettings.vSyncCount = 1;
    }
    public void VsyncClose() {
        QualitySettings.vSyncCount = 0;
    }
}
