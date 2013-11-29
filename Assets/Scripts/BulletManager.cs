using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//子弹管理器
public class BulletManager : MonoBehaviour
{
	//子弹池
	private List<Bullet> bulletPool = new List<Bullet>();

	//清理时间
	public float clearTime = 5;
	private float clearTick;
	
	void Update()
	{
		if(bulletPool.Count == 0)
		{
			return;
		}
		
		if(clearTick >= clearTime)
		{
			Bullet bullet;
			for(int i = 0;i < bulletPool.Count;)
			{
				bullet = bulletPool[i];
				if(bullet.gameObject.activeSelf == false)
				{
					bulletPool.Remove(bullet);
					Destroy(bullet.gameObject);
				}
				else
				{
					i++;
				}
			}
			clearTick = 0;
		}
		else
		{
			clearTick += Time.deltaTime;
		}
	}
	
	//添加子弹
	public void AddBullet(GameObject bullet,Character character,Vector3 positon,Vector2 direction)
	{
		clearTick = 0;
		foreach(Bullet b in bulletPool)
		{
			if(b.gameObject.activeSelf == false)
			{
				b.InitBullet(character,positon,direction);
				return;
			}
		}
		
		Bullet temp = (Instantiate(bullet) as GameObject).GetComponent<Bullet>();
		temp.transform.parent = transform;
		bulletPool.Add(temp);
		temp.InitBullet(character,positon,direction);
	}
}