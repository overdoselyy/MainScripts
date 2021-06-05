using UnityEngine;
using System.Collections;
/// <summary>
/// 绘制准星
/// </summary>
public class croos_hair : MonoBehaviour
{
	public float width;
	public float length;
	public float distance;
	public Texture2D crosshairTexture;
	public Texture2D crosshairTexture2;
	public GUIStyle lineStyle;
	private Texture tex;
	void Start()
	{
		lineStyle = new GUIStyle();
		lineStyle.normal.background = crosshairTexture;
	}
	private void OnGUI()
	{
		if (Input.GetKey(KeyCode.Mouse1))  
		{
			//根据坐标转换  离屏幕中心距离 绘制准星
			GUI.Box(new Rect((Screen.width - distance) / 2 - length, (Screen.height - width) / 2, length, width), tex, lineStyle);
			GUI.Box(new Rect((Screen.width + distance) / 2, (Screen.height - width) / 2, length, width), tex, lineStyle);
			GUI.Box(new Rect((Screen.width - width) / 2, (Screen.height - distance) / 2 - length, width, length), tex, lineStyle);
			GUI.Box(new Rect((Screen.width - width) / 2, (Screen.height + distance) / 2, width, length), tex, lineStyle);
		}
	}
}
