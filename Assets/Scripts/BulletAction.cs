using UnityEngine;
using System.Collections;

//子弹脚本
public class BulletAction : MonoBehaviour
{
	//飞行速度
	public float velocity = 80;

	//生命周期
	public float lifeTime = 2;

	//子弹发射者
	private CharacterAction shooter;

	void Awake()
	{
		gameObject.name = "Bullet";
		transform.parent = GameObject.Find("Bullets").transform;
	}

	void Update()
	{
		lifeTime -= Time.deltaTime;
		if(lifeTime <= 0)
		{
			Destroy(gameObject);
		}
	}

	//发射
	public void Shoot(CharacterAction shooter,Vector2 vector)
	{
		this.shooter = shooter;
		rigidbody2D.velocity = vector * velocity;
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.layer == LayerMask.NameToLayer("Terrain"))
		{
			Destroy(gameObject);
		}
		else if(c.gameObject.layer == LayerMask.NameToLayer("Character") && c.gameObject != shooter.gameObject)
		{
			Destroy(gameObject);
		}
	}
}