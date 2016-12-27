using UnityEngine;
using System.Collections;

public class PlayerGetCam : MonoBehaviour
{
	public Transform playerCanvas;

	void Start ()
	{
		GetCamera ();
	}

	public void GetCamera ()
	{
		Camera.main.transform.SetParent (playerCanvas);
		Camera.main.transform.position = Vector2.zero;
		playerCanvas.rotation = Quaternion.identity;
	}
}
