using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家信息重置
/// </summary>
public class PlayerReset : MonoBehaviour
{
    RagdollUtility ragdollUtility;
    public FullBodyBipedIK bipedIK;
    public PlayerInfo _playerInfo;
    public Resurgence _resurgence;
    public Move _move;
    public ShootControl _shootControl;
    public croos_hair _croos_Hair;
    public bool dead = false;
    void Start()
    {
        ragdollUtility = GetComponent<RagdollUtility>();
        ragdollUtility.EnableRagdoll();
        StartCoroutine("changeSkin");
    }
    void Update()
    {
       if (_resurgence.isReset)
        {
            StartCoroutine("changeSkin2");
        }
    }
    IEnumerator changeSkin()  //协程方法
    {
        yield return new WaitForSeconds(0.2f); 
        ragdollUtility.DisableRagdoll();
        bipedIK.enabled = true;
    }
    IEnumerator changeSkin2()  //协程方法
    {

        yield return new WaitForSeconds(2f);  
        CharacterController controller = GetComponent<CharacterController>();
        controller.enabled = true;
        _move.enabled = true;
        _resurgence.isReset = false;
        _shootControl.enabled = true;
        _croos_Hair.enabled = true;
        dead = false;
        _resurgence.exit = false;

    }
}
