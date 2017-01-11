using UnityEngine;
using System.Collections;

public class CanvasFollowPlayer : MonoBehaviour
{
	public Transform player;
	public Vector3 offset;

	private Transform _trans;

	void Start ()
	{
		_trans = transform;
		if (!player)
			player = GameObject.FindGameObjectWithTag ("Player").transform;

		offset = _trans.position - player.position;
	}

	void Update ()
	{
		Vector3 moveTo = offset + player.position;

		if (GenerateForFree.generateMap) {
			float maxX = GenerateForFree.generateMap.topEdgeSize;
			float maxY = GenerateForFree.generateMap.leftEdgeSize;

			moveTo.x = Mathf.Clamp (moveTo.x, -maxX, maxX);
			moveTo.y = Mathf.Clamp (moveTo.y, maxY, -maxY);
		}

		_trans.position = moveTo;
	}
}
