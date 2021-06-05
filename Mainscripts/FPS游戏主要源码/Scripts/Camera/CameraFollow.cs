using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制摄像机
/// 模拟后坐力
/// </summary>
public class CameraFollow : MonoBehaviour
{
	SpringEulerAngle recoilTarget;
	public float damp = 1;
	public float frequence = 5;
	void Awake()
	{
		recoilTarget = new SpringEulerAngle(damp, frequence);
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		recoilTarget.Update(Time.deltaTime, Vector3.zero);
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(recoilTarget.value), 0.1f);  //这里使用一个较大的插值数字以实现枪口的迅速抬升
	}

	void Update()
	{
		if (Input.GetButton("Fire1"))
		{
			recoilTarget.value = new Vector3(Random.Range(-5, 0), Random.Range(-3, 3), 0); //这里做一个随机偏移来模拟后坐力。
		}
	}
	public class Spring3
	{
		public Vector3 value = Vector2.zero;
		private Vector3 dampValue = Vector2.zero;
		private float damp = 1;
		private float frequence = 1;
		public void Clear()
		{
			value = Vector2.zero;
			dampValue = Vector2.zero;
		}
		public Spring3(float damp, float frequence)
		{
			this.damp = damp;
			this.frequence = frequence;
		}
		public void Update(float deltaTime, Vector3 target)
		{
			value -= dampValue * deltaTime * frequence;
			dampValue = Vector3.Lerp(dampValue, value - target, deltaTime * damp);
		}
	}
	public class SpringEulerAngle
	{
		public Vector3 value = Vector2.zero;
		private Vector3 dampValue = Vector2.zero;
		private float damp;
		private float frequence = 1;


		public SpringEulerAngle(float damp, float frequence)
		{
			this.damp = damp;
			this.frequence = frequence;
		}


		public void Clear()
		{
			value = Vector2.zero;
			dampValue = Vector2.zero;
		}


		public void Update(float deltaTime, Vector3 target)
		{
			value -= dampValue * deltaTime * frequence;
			dampValue = eulerLerp(dampValue, value - target, deltaTime * damp);
		}


		public static Vector3 eulerLerp(Vector3 left, Vector3 right, float t)
		{
			Vector3 ret;
			ret.x = Mathf.LerpAngle(left.x, right.x, t);
			ret.y = Mathf.LerpAngle(left.y, right.y, t);
			ret.z = Mathf.LerpAngle(left.z, right.z, t);
			return ret;
		}
	}
}