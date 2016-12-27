using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissilePooling : MonoBehaviour
{
	public static MissilePooling pool;
	public GameObject missile;
	public bool loadDone;
	public List<MissileController> missileInStorage;

	void Start ()
	{
		if (!pool) {
			pool = this;
		} else {
			Debug.Log ("Pool is orcus");
			DestroyImmediate (this.gameObject);
		}

		missileInStorage = new List<MissileController> ();
		StartCoroutine (SpawnMissile (50));
	}

	public IEnumerator SpawnMissile (int number)
	{
		loadDone = false;
		for (int i = 0; i < number; i++) {
			GameObject go = (GameObject)Instantiate (missile, transform);
			yield return new WaitForSeconds (0.005f);
			MissileController m = go.GetComponent<MissileController> ();
			if (m)
				ReturnLaser (m);
		}
		loadDone = true;
	}

	//	[RPC]
	//	private void RequestSpawn ()
	//	{
	//		GameObject go = (GameObject)Network.Instantiate (missile, transform);
	//		yield return new WaitForSeconds (0.005f);
	//		NetworkServer.Spawn (go);
	//		MissileController m = go.GetComponent<MissileController> ();
	//		if (m)
	//			ReturnLaser (m);
	//	}

	public MissileController GetMissile ()
	{
		MissileController result = null;

		if (missileInStorage.Count > 0) {
			result = missileInStorage [0];
			result.transform.SetParent (null);
			missileInStorage.RemoveAt (0);
		} else {
			StartCoroutine (SpawnMissile (3));
		}

		return result;
	}

	public void ReturnLaser (MissileController m)
	{
		m.transform.SetParent (transform);
		m.gameObject.SetActive (false);
		missileInStorage.Add (m);
	}
}
