using UnityEngine;
using System.Collections;

//敌人控制脚本
public class EnemyController : MonoBehaviour
{
	//角色对象
	private Character character;
	public Character Character{get{return character;}}
	
	void Start()
	{
		character = GetComponent<Character>();
	}
}