using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// 异步加载场景
/// </summary>
public class LoadSence : MonoBehaviour
{
		private AsyncOperation asy;
		public Slider progressB;
		void Start()
		{
			if (progressB)
			{
				progressB.value = 0;
			}
			StartCoroutine("LoadScene");
		}
		void Update()
		{
			Debug.Log(asy.progress);
			if (asy.progress < 0.9f)
			{
				progressB.value = asy.progress;
			}
			else
			{
				progressB.value = 1;
				asy.allowSceneActivation = true;
			}
		}
		IEnumerator LoadScene()
		{

		//u3d 5.3之后使用using UnityEngine.SceneManagement加载场景

		asy = SceneManager.LoadSceneAsync("Main");
		//不允许加载完毕自动切换场景，因为有时候加载太快了就看不到加载进度条UI效果了

		asy.allowSceneActivation = false;
			Debug.Log(asy.progress);
			yield return asy;
		}
	}


