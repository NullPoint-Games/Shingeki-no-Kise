using UnityEngine;
using System.Collections;

//角色脚本
public class CharacterAction : MonoBehaviour
{
	//角色属性
	public CharacterAttribute attribute;

	//脚部触发器
	public Transform footTrigger;
	//脸部触发器
	public Transform faceTrigger_Start;
	public Transform faceTrigger_End;

	//子弹发射位置
	public Transform ShootPosition;
	//子弹对象
	public GameObject bullet;

	//当前跳跃阶段
	private int jumpSection = 1;

	//当前攻击CD
	private float ACD;

	void Update()
	{
		if(ACD > 0)
		{
			ACD -= Time.deltaTime;
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
			rigidbody2D.AddForce(vector * attribute.runForce);
			rigidbody2D.velocity = new Vector2(Mathf.Clamp(rigidbody2D.velocity.x,-attribute.runVelocity,attribute.runVelocity),rigidbody2D.velocity.y);
		}
	}
	
	//跳跃
	public void Jump()
	{
		if(IsOnGround())
		{
			jumpSection = 1;
		}
		else if(jumpSection > attribute.jumpSection)
		{
			return;
		}

		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,attribute.jumpVelocity);
		jumpSection++;
	}

	//发射子弹
	public void Shoot()
	{
		if(ACD <= 0)
		{
			ACD = attribute.ACD;
			GameObject b = Instantiate(bullet) as GameObject;
			b.transform.position = ShootPosition.position;
			b.GetComponent<BulletAction>().Shoot(this,new Vector2(transform.localScale.x,0));
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