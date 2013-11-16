using UnityEngine;
using System.Collections;

//玩家脚本
public class PlayerAction : CharacterAction
{
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
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}

		if(Input.GetKey(KeyCode.A))
		{
			Run(RunDirection.Left);
		}
		else if(Input.GetKey(KeyCode.D))	
		{
			Run(RunDirection.Right);
		}
	}
	#endif
	
	//处理摇杆事件
	private void On_JoystickMove(MovingJoystick joystick)
	{
		if(joystick.joystickAxis.x < 0)
		{
			Run(RunDirection.Left);
		}
		else if(joystick.joystickAxis.x > 0)	
		{
			Run(RunDirection.Right);
		}
	}

	//处理按钮事件
	private void On_ButtonDown(string btn)
	{
		Jump();
	}
}