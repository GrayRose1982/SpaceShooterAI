using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterPooling : MonoBehaviour
{
	public static CharacterPooling pool;

	public GameObject ship;
	public bool loadDone;

	public List<AIMoveController> ships;

	void Start ()
	{
		ships = new List<AIMoveController> ();
		pool = this;

		StartCoroutine (SpawnShip (20));
	}

	public IEnumerator SpawnShip (int number)
	{
		loadDone = false;
		for (int i = 0; i < number; i++) {
			GameObject go = (GameObject)Instantiate (ship, transform);
			yield return new WaitForSeconds (0.005f);
			AIMoveController s = go.GetComponent<AIMoveController> ();
			if (s)
				ReturnShip (s);
		}
		loadDone = true;
	}

	//	public IEnumerator SpawnAndGetShip ()
	//	{
	//		GameObject go = (GameObject)Instantiate (ship);
	//		yield return new WaitForSeconds (0.005f);
	//		AIMoveController s = go.GetComponent<AIMoveController> ();
	//		yield return s;
	//	}

	public AIMoveController GetShip ()
	{
		AIMoveController result = null;

		if (ships.Count > 0) {
			result = ships [0];
			result.transform.SetParent (null);
			ships.RemoveAt (0);
		} else {
			StartCoroutine (SpawnShip (3));
		}

		return result;
	}

	public void ReturnShip (AIMoveController s)
	{
		if (!s)
			return;

		s.transform.SetParent (transform);
		s.gameObject.SetActive (false);
		ships.Add (s);
	}
}
