using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeteorPooling : MonoBehaviour
{
	public static MeteorPooling pool;

	public GameObject meteor;
	public bool loadDone;

	public List<AICharacter> meteors;

	void Start ()
	{
		meteors = new List<AICharacter> ();
		pool = this;

		StartCoroutine (SpawnMeteor (50));
	}

	public IEnumerator SpawnMeteor (int number)
	{
		loadDone = false;
		for (int i = 0; i < number; i++) {
			GameObject go = (GameObject)Instantiate (meteor, transform);
			yield return new WaitForSeconds (0.005f);
			AICharacter s = go.GetComponent<AICharacter> ();
			if (s)
				ReturnMeteor (s);
		}
		loadDone = true;
	}


	public AICharacter GetMeteor ()
	{
		AICharacter result = null;

		if (meteors.Count > 0) {
			result = meteors [0];
			result.transform.SetParent (null);
			meteors.RemoveAt (0);
		} else {
			StartCoroutine (SpawnMeteor (30));
		}

		return result;
	}

	public void ReturnMeteor (AICharacter s)
	{
		s.transform.SetParent (transform);
		s.gameObject.SetActive (false);
		meteors.Add (s);
	}
}
