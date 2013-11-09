using UnityEngine;
using System.Collections;

//小鸡脚本
public class ChickAction : MonoBehaviour
{
	//最大奔跑速度
	public float maxRunSpeed = 8;
	//奔跑力度
	public float runForce = 500;
	//跳跃力度
	public float jumpForce = 1200;
	//跳跃标记
	private bool isJump = false;

	void Update()
	{
		#if UNITY_EDITOR || UNITY_STANDALONE
		if(Input.GetKeyDown(KeyCode.Space) && !isJump)
		{
			Jump();
		}
		#endif
	}
	
	void FixedUpdate()
	{
		CheckOnGround();

		#if UNITY_EDITOR || UNITY_STANDALONE
		if(Input.GetKey(KeyCode.A))
		{
			Run(-Vector2.right);
		}
		else if(Input.GetKey(KeyCode.D))	
		{
			Run(Vector2.right);
		}
		#endif
	}

	//检查是否着地
	private void CheckOnGround()
	{
		isJump = !Physics2D.Linecast(transform.position,transform.position,1 << LayerMask.NameToLayer("Terrain"));  
	}

	//奔跑
	private void Run(Vector2 dir)
	{
		transform.localScale = new Vector3(dir.x,1,1);

		rigidbody2D.AddForce(dir * runForce);

		if(Mathf.Abs(rigidbody2D.velocity.x) > maxRunSpeed)
		{
			rigidbody2D.velocity = new Vector2(dir.x * maxRunSpeed,rigidbody2D.velocity.y);
		}
	}

	//跳跃
	private void Jump()
	{
		rigidbody2D.AddForce(Vector2.up * jumpForce);
		isJump = true;
	}
}