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
		if(Input.GetKey(KeyCode.W))	
		{
			transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.S))
		{
			transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
		}

		if(Input.GetKey(KeyCode.D))	
		{
			transform.localScale = new Vector3(1,1,1);
			transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.A))
		{
			transform.localScale = new Vector3(-1,1,1);
			transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
		}
	}
}