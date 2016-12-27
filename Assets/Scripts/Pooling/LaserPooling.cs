using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserPooling : MonoBehaviour
{
	public static LaserPooling pool;
	public GameObject laser;
	public bool loadDone;
	public List<LaserController> laserInStorage;

	void Start ()
	{
		laserInStorage = new List<LaserController> ();
		pool = this;

		StartCoroutine (SpawnLaser (50));
	}

	public IEnumerator SpawnLaser (int number)
	{
		loadDone = false;
		for (int i = 0; i < number; i++) {
			GameObject go = (GameObject)Instantiate (laser, transform);
			yield return new WaitForSeconds (0.005f);
			LaserController l = go.GetComponent<LaserController> ();
			if (l)
				ReturnLaser (l);
		}
		loadDone = true;
	}

	public LaserController GetLaser ()
	{
		LaserController result = null;

		if (laserInStorage.Count > 0) {
			result = laserInStorage [0];
			result.transform.SetParent (null);
			laserInStorage.RemoveAt (0);
		} else {
			StartCoroutine (SpawnLaser (3));
		}

		return result;
	}

	public void ReturnLaser (LaserController l)
	{
		l.transform.SetParent (transform);
		l.gameObject.SetActive (false);
		laserInStorage.Add (l);
	}
}
