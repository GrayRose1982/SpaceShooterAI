using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
	public static UIScore score;
	[SerializeField] private  Text scoreText;

	void Start ()
	{
		if (!score)
			score = this;
	}

	public void SetScore (int newScore)
	{
		scoreText.text = newScore.ToString ();
	}
}
