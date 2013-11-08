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

	//碰撞检测
	void OnCollisionEnter2D(Collision2D c)
	{
		print("hit object:" + c.transform.name);
	}
	
	//处理键盘事件
	private void HandleKeyboard()
	{
		if(Input.GetKey(KeyCode.W))	
		{
			MoveToDirection(Vector3.up);
		}
		else if(Input.GetKey(KeyCode.S))
		{
			MoveToDirection(Vector3.down);
		}

		if(Input.GetKey(KeyCode.A))
		{
			MoveToDirection(Vector3.left);
		}
		else if(Input.GetKey(KeyCode.D))	
		{
			MoveToDirection(Vector3.right);
		}
	}

	//处理鼠标事件
	private void HandleMouse()
	{
		if(!Input.GetMouseButton(0))
		{
			return;
		}
		
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = transform.position.z;
		MoveToPoint(pos);
	}

	//移动方向
	private void MoveToDirection(Vector3 direction)
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

	//移动到指定点
	private void MoveToPoint(Vector3 pos)
	{
		if(transform.position.x - pos.x < 0)
		{
			transform.localScale = new Vector3(1,1,1);
		}
		else if(transform.position.x - pos.x > 0)
		{
			transform.localScale = new Vector3(-1,1,1);
		}

		transform.position = Vector3.Lerp(transform.position,pos,moveSpeed * Time.deltaTime);
	}
}