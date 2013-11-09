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
	//脚部
	public Transform foot;

	void Update()
	{
		#if UNITY_EDITOR || UNITY_STANDALONE
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}
		#endif
	}
	
	void FixedUpdate()
	{
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

	private bool CheckOnGround()
	{
		return Physics2D.Linecast(transform.position,foot.position,1 << LayerMask.NameToLayer("Terrain"));
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
		if(!CheckOnGround())
		{
			return;
		}

		rigidbody2D.AddForce(Vector2.up * jumpForce);
	}
}