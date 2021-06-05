using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 布娃娃开启 关闭
/// </summary>
public class Ragdoll : MonoBehaviour
{
    RagdollUtility ragdollUtility;
    public bool dead;
    public FullBodyBipedIK bipedIK;
    void Start()
    {
        ragdollUtility=GetComponent<RagdollUtility>();
        dead = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) {
            dead = !dead;
            if (dead)
            {
                ragdollUtility.EnableRagdoll();

            }
            else {
                ragdollUtility.DisableRagdoll();
                bipedIK.enabled = true;
            }
        }
    }
}
