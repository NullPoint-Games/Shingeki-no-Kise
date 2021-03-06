using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//消息中心
public class NotificationCenter : MonoBehaviour
{
	//实力句柄
	private static NotificationCenter instance;
	
	//消息队列
	private List<Notification> notifications = new List<Notification>();
	
	//注册消息
	public static void Register(string name,MonoBehaviour receiver)
	{
		foreach(Notification n in GetInstance().notifications)
		{
			if(n.Name == name)
			{
				n.AddReceiver(receiver);
				return;
			}
		}
		
		Notification temp = new Notification(name);
		temp.AddReceiver(receiver);
		GetInstance().notifications.Add(temp);
	}
	
	//派发消息
	public static void Dispatch(string name,MonoBehaviour dispatcher)
	{
		foreach(Notification n in GetInstance().notifications)
		{
			if(n.Name == name)
			{
				foreach(MonoBehaviour receiver in n.Receivers)
				{
					receiver.SendMessage(name,dispatcher,SendMessageOptions.DontRequireReceiver);
				}
				return;
			}
		}
	}
	
	//清空消息队列
	public static void ClearNotifications()
	{
		GetInstance().notifications.Clear();
	}
	
	//获取实力句柄
	private static NotificationCenter GetInstance()
	{
		if(instance == null)
		{
			GameObject mount = new GameObject();
			mount.name = "NotificationCenter";
			instance = mount.AddComponent<NotificationCenter>();
		}
		return instance;
	}
	
	//消息体
	private class Notification
	{
		//消息名
		private string _name;
		public string Name{get{return _name;}}
		
		//接收者
		private List<MonoBehaviour> _receivers = new List<MonoBehaviour>();
		public List<MonoBehaviour> Receivers{get{return _receivers;}}
		
		public Notification(string name)
		{
			_name = name;
		}
		
		//添加接收者
		public void AddReceiver(MonoBehaviour receiver)
		{
			_receivers.Add(receiver);
		}
	}
}