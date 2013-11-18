using UnityEngine;
using System.Collections;

//角色属性
public class CharacterAttribute : MonoBehaviour
{
	//奔跑力度(调节加速度)
	public float runForce = 200;
	//最大奔跑速度
	public float runVelocity = 12;
	
	//跳跃初速度(调节跳跃高度)
	public float jumpVelocity = 20;
	//跳跃阶段数
	public int jumpSection = 2;

	//血量
	public int HP;
	//攻击力
	public int ATK;
	//攻击CD
	public float ACD;
}