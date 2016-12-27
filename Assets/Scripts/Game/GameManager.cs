using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	void Start ()
	{
		if (!instance) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			DestroyImmediate (gameObject);
		}
	}
}