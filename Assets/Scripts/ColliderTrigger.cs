using UnityEngine;
using System.Collections;

public enum ColliderState
{
	Enter,
	Stay,
	Exit,
}

//碰撞触发器
public class ColliderTrigger : MonoBehaviour
{
	public GameObject listener;

	private ColliderState _state;
	private ColliderState _prevState;
	public ColliderState State
	{
		private set
		{
			_prevState = _state;
			_state = value;
		}
		get
		{
			return _state;
		}
	}
	public ColliderState PrevState
	{
		get{return _prevState;}
	}
	
	void OnCollisionEnter(Collision c)
	{
		State = ColliderState.Enter;
		listener.SendMessage("OnCollisionEnter_" + name,c,SendMessageOptions.DontRequireReceiver);
	}
	void OnCollisionEnter2D(Collision2D c)
	{
		State = ColliderState.Enter;
		listener.SendMessage("OnCollisionEnter2D_" + name,c,SendMessageOptions.DontRequireReceiver);
	}
	
	void OnCollisionStay(Collision c)
	{
		State = ColliderState.Stay;
		listener.SendMessage("OnCollisionStay_" + name,c,SendMessageOptions.DontRequireReceiver);
	}
	void OnCollisionStay2D(Collision2D c)
	{
		State = ColliderState.Stay;
		listener.SendMessage("OnCollisionStay2D_" + name,c,SendMessageOptions.DontRequireReceiver);
	}
	
	void OnCollisionExit(Collision c)
	{
		State = ColliderState.Exit;
		listener.SendMessage("OnCollisionExit_" + name,c,SendMessageOptions.DontRequireReceiver);
	}
	void OnCollisionExit2D(Collision2D c)
	{
		State = ColliderState.Exit;
		listener.SendMessage("OnCollisionExit2D_" + name,c,SendMessageOptions.DontRequireReceiver);
	}
	
	void OnTriggerEnter(Collider c)
	{
		State = ColliderState.Enter;
		listener.SendMessage("OnTriggerEnter_" + name,c,SendMessageOptions.DontRequireReceiver);
	}
	void OnTriggerEnter2D(Collider2D c)
	{
		State = ColliderState.Enter;
		listener.SendMessage("OnTriggerEnter2D_" + name,c,SendMessageOptions.DontRequireReceiver);
	}
	
	void OnTriggerStay(Collider c)
	{
		State = ColliderState.Stay;
		listener.SendMessage("OnTriggerStay_" + name,c,SendMessageOptions.DontRequireReceiver);
	}
	void OnTriggerStay2D(Collider2D c)
	{
		State = ColliderState.Stay;
		listener.SendMessage("OnTriggerStay2D_" + name,c,SendMessageOptions.DontRequireReceiver);
	}
	
	void OnTriggerExit(Collider c)
	{
		State = ColliderState.Exit;
		listener.SendMessage("OnTriggerExit_" + name,c,SendMessageOptions.DontRequireReceiver);
	}
	void OnTriggerExit2D(Collider2D c)
	{
		State = ColliderState.Exit;
		listener.SendMessage("OnTriggerExit2D_" + name,c,SendMessageOptions.DontRequireReceiver);
	}
}