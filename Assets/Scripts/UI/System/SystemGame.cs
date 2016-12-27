using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SystemGame : MonoBehaviour
{
	public void btn_LoadScene (string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}

	public void btn_SetTimeScale (float timeScale)
	{
		Time.timeScale = timeScale;
	}
}
