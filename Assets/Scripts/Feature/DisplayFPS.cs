using UnityEngine;
using System.Collections;

public class DisplayFPS : MonoBehaviour
{
	//目标刷新率
	public int targetFPS = 60;

	//刷新频率
	public float time = 0.5f;
	//上次时间
	private float lastTime;

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
		
		lastTime = Time.time;
	}
	
	void OnGUI()
	{
		GUI.color = color;
		GUI.Label(new Rect(position.x,position.y,200,200),"FPS:" + fps);
	}
	
	void Update()
	{
		++frames;
		
		float timeDiff = Time.time - lastTime;
		if(timeDiff >= time)
		{
			lastTime = Time.time;
			fps = (int)(frames / timeDiff);
			frames = 0;
		}
	}
}