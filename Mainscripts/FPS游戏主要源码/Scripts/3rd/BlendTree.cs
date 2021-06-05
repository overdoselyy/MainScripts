using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
/// <summary>
/// 动画混合树
/// </summary>
[RequireComponent(typeof(Animator))]
public class BlendTree : MonoBehaviour {
	Animator anim = null;
	void Start () {
		anim=GetComponent<Animator>();
	}
	void Update() 
	{
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");
		Vector3 move = v * Vector3.forward + h * Vector3.right;
		if (Input.GetKey(KeyCode.LeftShift)) move.z *= 0.5f;
		float turn = move.x;
		float forward = move.z;
		anim.SetFloat("speed", forward, 0.1f, Time.deltaTime);
		anim.SetFloat("turn", turn, 0.1f, Time.deltaTime);
		if (Input.GetMouseButton(1))
		{
			anim.SetBool("shoot", true);
		}
		else {
			anim.SetBool("shoot", false);

		}
		if (Input.GetKey(KeyCode.LeftControl))
		{
			anim.SetFloat("crouch", 1f, 0.1f, Time.deltaTime);
		}
		else
		{
			anim.SetFloat("crouch",0f);

		}
	}
}
