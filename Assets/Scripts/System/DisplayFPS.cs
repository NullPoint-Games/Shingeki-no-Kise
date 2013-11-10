using UnityEngine;
using System.Collections;

public class DisplayFPS : MonoBehaviour
{
	//目标刷新率
	public int targetFPS = 0;

	//刷新频率
	public float interval = 0.5f;

	//位置
	public Vector2 position = new Vector2(10,10);

	//字体颜色
	public Color color = Color.white;
	
	private int frames = 0;
	
	private int fps;
	
	void Start() 
	{
		//设置目标帧率
		if(targetFPS != 0)
		{
			Application.targetFrameRate = targetFPS;
		}

		InvokeRepeating("UpdateFPS",0,interval);
	}
	
	void OnGUI() 
	{
		GUI.color = color;
		GUI.Label(new Rect(position.x,position.y,200,200),"FPS:" + fps);
	}
	
	void Update() 
	{
		++frames;
	}

	private void UpdateFPS()
	{
		fps = (int)(frames / interval);
		frames = 0;
	}
}