using UnityEngine;
using System.Collections;

[System.Serializable]
public class EngineEntity
{
	public string id;
	public string name;

	public float maxSpeed;
	public float minSpeed;

	public float angleRotate;

	public EngineEntity ()
	{
		
	}

	public EngineEntity (EngineEntity e)
	{
		id = e.id;
		name = e.name;
		maxSpeed = e.maxSpeed;
		minSpeed = e.minSpeed;
		angleRotate = e.angleRotate;
	}
}
