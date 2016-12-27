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
		_trans.position = offset + player.position;
	}
}
