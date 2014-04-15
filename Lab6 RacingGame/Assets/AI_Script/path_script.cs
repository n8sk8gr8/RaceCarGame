using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class path_script : MonoBehaviour {
	
	public Transform[] path;
	public Transform[] path_objs;
	public Color rayColor = Color.white;
	public Vector3 pos;
	public Vector3 prev;
	
	void OnDrawGizmos()
	{
		Gizmos.color = rayColor;
		//pathTransform = 
		path_objs = this.GetComponentsInChildren <Transform>();
		path = new Transform[path_objs.Length];

		for (int i = 0; i < path_objs.Length; i++) 
		{
			path[i] = path_objs[i];
		}
		
		for (int i=0; i<path.Length; i++) 
		{
			pos = path[i].position;
			if(i>0)
			{
				pos = path[i].position;
				if(i>0)
				{
					prev = path[i-1].position;
				}
				Gizmos.DrawLine(prev,pos);
				Gizmos.DrawWireSphere(pos,0.3f);
			}
		}
	}
}
