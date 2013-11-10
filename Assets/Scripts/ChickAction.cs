using UnityEngine;
using System.Collections;

//小鸡状态
public enum ChickJumpState
{
	Jump,
	SecondJump,
}

//小鸡脚本
public class ChickAction : MonoBehaviour
{
	//最大奔跑速度
	public float maxVelocity = 8;
	//奔跑力度
	public float runForce = 350;
	//跳跃力度
	public float jumpForce = 1200;
	//脸部触发器
	public Transform faceTrigger;
	//脚部触发器
	public Transform footTrigger;

	//跳跃状态
	private ChickJumpState _jumpState;
	public ChickJumpState JumpState
	{
		private set{_jumpState = value;}
		get{return _jumpState;}
	}

	void Update()
	{
		#if UNITY_EDITOR || UNITY_STANDALONE
		HandleKeyboardEvent();
		#endif
	}

	//处理键盘事件
	private void HandleKeyboardEvent()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}
		
		if(Input.GetKey(KeyCode.A))
		{
			Run(-Vector2.right);
		}
		else if(Input.GetKey(KeyCode.D))	
		{
			Run(Vector2.right);
		}
	}

	//处理摇杆事件
	private void On_JoystickMove(MovingJoystick joystick)
	{
		if(joystick.joystickAxis.x < 0)
		{
			Run(-Vector2.right);
		}
		else if(joystick.joystickAxis.x > 0)	
		{
			Run(Vector2.right);
		}
	}

	//处理按钮事件
	private void On_ButtonDown(string btn)
	{
		Jump();
	}

	//奔跑
	private void Run(Vector2 dir)
	{
		transform.localScale = new Vector3(dir.x,1,1);
		dir.y = 0;

		if(CheckFaceTrigger())
		{
			return;
		}

		rigidbody2D.AddForce(dir * runForce);
		rigidbody2D.velocity = new Vector2(Mathf.Clamp(rigidbody2D.velocity.x,-maxVelocity,maxVelocity),rigidbody2D.velocity.y);
	}

	//跳跃
	private void Jump()
	{
		if(CheckFootTrigger())
		{
			JumpState = ChickJumpState.Jump;
		}
		else if(JumpState == ChickJumpState.Jump)
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,0);
			JumpState = ChickJumpState.SecondJump;
		}
		else if(JumpState == ChickJumpState.SecondJump)
		{
			return;
		}

		rigidbody2D.AddForce(Vector2.up * jumpForce);
	}

	//检查是否脸部碰到地形
	private bool CheckFaceTrigger()
	{
		return Physics2D.Linecast(faceTrigger.position - Vector3.up,faceTrigger.position + Vector3.up,1 << LayerMask.NameToLayer("Terrain"));
	}

	//检查是否碰到地面
	private bool CheckFootTrigger()
	{
		return Physics2D.Linecast(transform.position,footTrigger.position,1 << LayerMask.NameToLayer("Terrain"));
	}
}