using UnityEngine;
using System.Collections;

public class UIGameController : MonoBehaviour
{
	public static UIGameController gameController;

	public GameObject gameOverPanel;
	public GameObject pausePanel;

	void Start ()
	{
		gameController = this;
	}

	public void GameOver ()
	{
		gameOverPanel.SetActive (true);
		pausePanel.SetActive (false);
	}

	public void Setting ()
	{
		gameOverPanel.SetActive (false);
		pausePanel.SetActive (true);
	}
}
