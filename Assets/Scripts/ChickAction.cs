using UnityEngine;
using System.Collections;

//小鸡脚本
public class ChickAction : MonoBehaviour
{
	//移动速度
	public float moveSpeed = 10;

	void Start()
	{
	}

	void Update()
	{
		HandleKeyboard();
		HandleMouse();
	}
	
	//处理键盘事件
	private void HandleKeyboard()
	{
		if(Input.GetKey(KeyCode.W))	
		{
			Move(Vector3.up);
		}
		else if(Input.GetKey(KeyCode.S))
		{
			Move(Vector3.down);
		}

		if(Input.GetKey(KeyCode.A))
		{
			Move(Vector3.left);
		}
		else if(Input.GetKey(KeyCode.D))	
		{
			Move(Vector3.right);
		}
	}

	//处理鼠标事件
	private void HandleMouse()
	{
		if(!Input.GetMouseButton(0))
		{
			return;
		}
		
		Vector3 dir = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
		dir.z = 0;
		Move(dir);
	}

	//移动
	private void Move(Vector3 direction)
	{
		if(direction.x < 0)
		{
			transform.localScale = new Vector3(-1,1,1);
		}
		else if(direction.x > 0)
		{
			transform.localScale = new Vector3(1,1,1);
		}
		
		transform.Translate(direction * moveSpeed * Time.deltaTime);
	}
}