using UnityEngine;
using System.Collections;

//子弹脚本
public class Bullet : MonoBehaviour
{
	//子弹归属者
	private Character character;

	//飞行速度
	public float velocity;
	//飞行方向
	private Vector2 direction;

	//生命周期
	public float lifeTime;
	private float lifeTick;

	//发射
	public void InitBullet(Character character,Vector3 position,Vector2 direction)
	{
		this.character = character;
		transform.position = position;
		this.direction = direction * velocity;
		rigidbody2D.velocity = this.direction;
		lifeTick = lifeTime;
		name = character.name + "_Bullet";
		gameObject.SetActive(true);
	}

	void Update()
	{
		if(lifeTick > 0)
		{
			lifeTick -= Time.deltaTime;
			if(lifeTick <= 0)
			{
				gameObject.SetActive(false);
			}
		}
		rigidbody2D.velocity = direction;
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject == character.gameObject)
		{
			return;
		}

		gameObject.SetActive(false);

		if(c.gameObject.layer == LayerMask.NameToLayer("Terrain"))
		{
			gameObject.SetActive(false);
		}
		else if(c.gameObject.layer == LayerMask.NameToLayer("Character"))
		{
			gameObject.SetActive(false);
		}
	}
}