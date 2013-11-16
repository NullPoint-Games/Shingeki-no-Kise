using UnityEngine;
using System.Collections;

//运动方向
public enum RunDirection
{
	Left,
	Right,
}

//角色脚本
public class CharacterAction : MonoBehaviour
{
	//脚部触发器
	public Transform footTrigger;
	//脸部触发器
	public Transform faceTrigger_Start;
	public Transform faceTrigger_End;

	//跳跃初速度
	public float jumpVelocity = 20;
	//最大奔跑速度
	public float runVelocity = 15;

	//跳跃阶段数
	public int jumpSection = 2;
	//当前跳跃阶段
	private int curJumpSection = 1;

	//奔跑
	public void Run(RunDirection rd)
	{
		FaceTo(rd);
		if(IsFaceToGround())
		{
			rigidbody2D.velocity = new Vector2(0,rigidbody2D.velocity.y);
		}
		else
		{
			rigidbody2D.velocity = new Vector2((rd == RunDirection.Left ? -1 : 1) * runVelocity,rigidbody2D.velocity.y);
		}
	}
	
	//跳跃
	public void Jump()
	{
		if(IsOnGround())
		{
			curJumpSection = 1;
		}
		else if(curJumpSection > jumpSection)
		{
			return;
		}

		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,jumpVelocity);
		curJumpSection++;
	}

	//设置面朝向
	public void FaceTo(RunDirection rd)
	{
		transform.localScale = new Vector3(rd == RunDirection.Left ? -1 : 1,1,1);
	}

	//判断是否着地
	public bool IsOnGround()
	{
		if(rigidbody2D.velocity.y != 0)
		{
			return false;
		}

		return Physics2D.Linecast(footTrigger.position,footTrigger.position,1 << LayerMask.NameToLayer("Terrain"));
	}
	//判断前方是否有墙壁
	public bool IsFaceToGround()
	{
		return Physics2D.Linecast(faceTrigger_Start.position,faceTrigger_End.position,1 << LayerMask.NameToLayer("Terrain"));
	}
}