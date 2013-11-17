using UnityEngine;
using System.Collections;

//玩家脚本
public class PlayerController : MonoBehaviour
{
	public CharacterAction action;

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
			action.Shoot();
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			action.Jump();
		}

		if(Input.GetKey(KeyCode.A))
		{
			action.Run(-Vector2.right);
		}
		else if(Input.GetKey(KeyCode.D))	
		{
			action.Run(Vector2.right);
		}
	}
	#endif
	
	//处理摇杆事件
	private void On_JoystickMove(MovingJoystick joystick)
	{
		if(joystick.joystickAxis.x < 0)
		{
			action.Run(-Vector2.right);
		}
		else if(joystick.joystickAxis.x > 0)	
		{
			action.Run(Vector2.right);
		}
	}

	//处理按钮事件
	private void On_ButtonDown(string btn)
	{
		action.Jump();
	}
}