using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SetUpForNetwork : NetworkBehaviour
{
	public Transform followPlayer;
	public GameObject playerCanvas;
	public MoveController moveController;
	public WeaponController weaponController;

	void Start ()
	{
		if (!isLocalPlayer) {
			playerCanvas.SetActive (false);
//			moveController.enabled = false;
		} else {
			playerCanvas.transform.rotation = Quaternion.identity;
			Camera.main.transform.SetParent (followPlayer);
			Camera.main.transform.localPosition = Vector2.zero;
			Camera.main.transform.localRotation = Quaternion.identity;
		}
	}

	void Update ()
	{
		if (!isLocalPlayer)
			return;

		weaponController.ControlFire ();
	}

	//	[Command]
	//	void CmdControlFire ()
	//	{
	//	}
}
