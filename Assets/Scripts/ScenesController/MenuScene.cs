using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
	public void btn_2player ()
	{
		SceneManager.LoadScene ("PlayTwoPlayerScene");
	}

	public void btn_Play ()
	{
		SceneManager.LoadScene ("PlayOnePlayerScene");
	}
}
