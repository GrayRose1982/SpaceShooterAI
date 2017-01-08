using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
	void Update ()
	{
		if (Input.touches.Length > 0 || Input.GetMouseButtonDown (0)) {
			SceneManager.LoadScene ("MainGame");
		}
	}
}
