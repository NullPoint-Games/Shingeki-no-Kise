using UnityEngine;
using System.Collections;

//角色状态
public enum CharacterState
{
	//待机
	Idle,
	//奔跑
	Run,
}

//角色属性
public struct CharacterAttribute
{
	//奔跑速度
	public float RSPD;
	//奔跑加速度
	public float RACL;
	//跳跃速度
	public float JSPD;
	//跳跃阶段数
	public int JS;
	
	//血量
	public int HP;
	//攻击力
	public int ATK;
	//攻击CD
	public float ACD;
}

//角色脚本
public class Character : MonoBehaviour
{
	//当前状态
	private CharacterState state;
	//上一次状态
	private CharacterState prevState;
	public CharacterState State
	{
		private set
		{
			if(state != value)
			{
				prevState = state;
				state = value;
			}
		}
		get
		{
			return state;
		}
	}
	public CharacterState PrevState{get{return prevState;}}

	//角色属性
	private CharacterAttribute attribute;
	public CharacterAttribute Attribute{get{return attribute;}}

	//脚部触发器
	public Transform footTrigger;
	//脸部触发器
	public Transform faceTrigger_Start;
	public Transform faceTrigger_End;

	//子弹发射位置
	public Transform shootPosition;
	//子弹对象
	public GameObject bullet;
	//子弹管理器
	private BulletManager bulletManager;

	//当前移动速度
	private float rspd;
	//当前加速度
	private float racl;
	//当前移动方向
	private Vector2 dir = Vector2.right;
	
	//当前跳跃阶段
	private int js = 1;

	//当前HP
	private int hp;
	public int HP{get{return hp;}}
	//获取总血量
	public int TotalHP{get{return attribute.HP;}}

	//当前攻击CD
	private float acd;

	public void InitCharacter(CharacterAttribute attribute)
	{
		this.attribute = attribute;
	}

	void Start()
	{
		GameObject manager = GameObject.Find("Bullets/" + bullet.name + "_BulletManager");
		
		if(manager == null)
		{
			manager = new GameObject();
			manager.name = bullet.name + "_BulletManager";
			manager.transform.parent = GameObject.Find("Bullets").transform;
			bulletManager = manager.AddComponent<BulletManager>();
		}
		else
		{
			bulletManager = manager.GetComponent<BulletManager>();
		}
	}

	void Update()
	{
		UpdateACD();
	}

	void FixedUpdate()
	{
		switch(state)
		{
		case CharacterState.Idle:
			UpdateIdle();
			break;
		case CharacterState.Run:
			UpdateRun();
			break;
		}
	}

	private void UpdateACD()
	{
		if(acd > 0)
		{
			acd -= Time.deltaTime;
		}
	}
	
	private void UpdateIdle()
	{
		if(rspd > 0)
		{
			racl += attribute.RACL * Time.deltaTime;
			rspd -= racl;
			if(rspd <= 0)
			{
				rspd = 0;
				racl = 0;
			}
		}
		rigidbody2D.velocity = new Vector2(dir.x * rspd,rigidbody2D.velocity.y);
	}
	
	private void UpdateRun()
	{
		if(rspd < attribute.RSPD)
		{
			racl += attribute.RACL * Time.deltaTime;
			rspd += racl;
			if(rspd >= attribute.RSPD)
			{
				rspd = attribute.RSPD;
				racl = 0;
			}
		}
		rigidbody2D.velocity = new Vector2(dir.x * rspd,rigidbody2D.velocity.y);
	}

	//待机
	public void Idle()
	{
		State = CharacterState.Idle;
	}
	
	//奔跑
	public void Run(Vector2 vector)
	{
		dir = vector.normalized;
		transform.localScale = new Vector3(vector.x,1,1);
		if(IsFaceToGround())
		{
			Idle();
		}
		else
		{
			State = CharacterState.Run;
		}
	}
	
	//跳跃
	public void Jump()
	{
		if(IsOnGround())
		{
			js = 1;
		}
		else if(js > attribute.JS)
		{
			return;
		}

		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,attribute.JSPD);
		js++;
	}

	//发射子弹
	public void Shoot()
	{
		if(acd <= 0)
		{
			acd = attribute.ACD;
			bulletManager.AddBullet(bullet,this,shootPosition.position,dir);
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