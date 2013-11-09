using UnityEngine;
using System.Collections;

//摄像机脚本
public class CameraAction : MonoBehaviour
{
	//观察目标对象
	public Transform target;

	public void LateUpdate()
	{
		transform.position = new Vector3(target.position.x,transform.position.y,transform.position.z);
	}
}