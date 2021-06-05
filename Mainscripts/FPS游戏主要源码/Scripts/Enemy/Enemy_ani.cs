using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
/// <summary>
/// 敌人动画控制
/// </summary>
[RequireComponent(typeof(Animator))]
public class Enemy_ani : MonoBehaviour
{
	Animator anim = null;
	// Use this for initialization
	void Start()
	{
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");
		Vector3 move = v * Vector3.forward + h * Vector3.right;
		//if (Input.GetKey(KeyCode.LeftShift)) move.z *= 0.5f;
		float turn = move.x;
		float forward = move.z;
		anim.SetFloat("speed", forward, 0.1f, Time.deltaTime);
		anim.SetFloat("turn", turn, 0.1f, Time.deltaTime);
	}
}