using UnityEngine;
using System.Collections;

//角色脚本
public class CharacterAction : MonoBehaviour
{
	//脚部触发器
	public Transform footTrigger;
	//脸部触发器
	public Transform faceTrigger_Start;
	public Transform faceTrigger_End;

	//子弹发射位置
	public Transform ShootPosition;
	//子弹对象
	public GameObject bullet;
	
	//奔跑力度
	public float runForce = 200;
	//最大奔跑速度
	public float runVelocity = 12;

	//跳跃初速度
	public float jumpVelocity = 20;

	//跳跃阶段数
	public int jumpSection = 2;
	//当前跳跃阶段
	private int curJumpSection = 1;

	//攻击CD
	public float attackCD = 1;
	private float attackCDTick;

	void Update()
	{
		if(attackCDTick > 0)
		{
			attackCDTick -= Time.deltaTime;
		}
	}
	
	//奔跑
	public void Run(Vector2 vector)
	{
		transform.localScale = new Vector3(vector.x,1,1);
		if(IsFaceToGround())
		{
			rigidbody2D.velocity = new Vector2(0,rigidbody2D.velocity.y);
		}
		else
		{
			rigidbody2D.AddForce(vector * runForce);
			rigidbody2D.velocity = new Vector2(Mathf.Clamp(rigidbody2D.velocity.x,-runVelocity,runVelocity),rigidbody2D.velocity.y);
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

	//发射子弹
	public void Shoot()
	{
		if(attackCDTick <= 0)
		{
			attackCDTick = attackCD;
			GameObject b = Instantiate(bullet) as GameObject;
			b.transform.position = ShootPosition.position;
			b.GetComponent<BulletAction>().Shoot(new Vector2(transform.localScale.x,0));
		}
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