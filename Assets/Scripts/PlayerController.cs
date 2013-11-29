using UnityEngine;
using System.Collections;

//玩家控制脚本
public class PlayerController : MonoBehaviour
{
	//角色对象
	private Character character;
	public Character Character{get{return character;}}

	void Start()
	{
		character = GetComponent<Character>();

		CharacterAttribute attribute = new CharacterAttribute();
		attribute.RSPD = 16;
		attribute.RACL = 8;
		attribute.JSPD = 20;
		attribute.JS = 2;
		attribute.ACD = 0.3f;

		character.InitCharacter(attribute);
	}

	void Update()
	{
		#if UNITY_EDITOR || UNITY_STANDALONE
		HandleKeyboardEvent();
		#endif
	}
	
	#if UNITY_EDITOR || UNITY_STANDALONE
	//处理键盘事件
	private void HandleKeyboardEvent()
	{
		if(Input.GetKey(KeyCode.Return))
		{
			character.Shoot();
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			character.Jump();
		}
		
		if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
		{
			character.Idle();
		}
		else if(Input.GetKey(KeyCode.A))
		{
			character.Run(-Vector2.right);
		}
		else if(Input.GetKey(KeyCode.D))	
		{
			character.Run(Vector2.right);
		}
	}
	#endif
	
	//处理摇杆事件
	private void On_JoystickMove(MovingJoystick joystick)
	{
		if(joystick.joystickAxis.x < 0)
		{
			character.Run(-Vector2.right);
		}
		else if(joystick.joystickAxis.x > 0)	
		{
			character.Run(Vector2.right);
		}
	}
	private void On_JoystickMoveEnd(MovingJoystick joystick)
	{
		character.Idle();
	}

	//处理按钮事件
	private void On_ButtonDown(GameObject btn)
	{
		switch(btn.name)
		{
		case "JumpButton":
			character.Jump();
			break;
		case "ShootButton":
			character.Shoot();
			break;
		}
	}
}