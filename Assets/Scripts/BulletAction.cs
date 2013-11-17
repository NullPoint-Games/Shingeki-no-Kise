using UnityEngine;
using System.Collections;

//子弹脚本
public class BulletAction : MonoBehaviour
{
	//飞行速度
	public float velocity = 80;

	void Awake()
	{
		gameObject.name = "Bullet";
		transform.parent = GameObject.Find("Bullets").transform;
	}

	//发射
	public void Shoot(Vector2 vector)
	{
		rigidbody2D.velocity = vector * velocity;
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.layer == LayerMask.NameToLayer("Terrain"))
		{
			Destroy(gameObject);
		}
	}
}