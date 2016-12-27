using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public static GameController gc;
	public int _coin;

	public int maxScore;
	public int maxLevel;

	void Start ()
	{
		if (!gc) {
			gc = this;
			DontDestroyOnLoad (gameObject);
		} else
			DestroyImmediate (gameObject);
	}

	public void AddCoin (int addNum)
	{
		_coin += addNum;
		UIScore.score.SetScore (_coin);
	}

	public bool SubCoin (int subNum)
	{
		if (subNum > _coin)
			return false;

		_coin -= subNum;
		UIScore.score.SetScore (_coin);
		return true;
	}

	public void ClearCoin ()
	{
		_coin = 0;
		UIScore.score.SetScore (_coin);
	}

	public void CheckMaxLevel (int newLevel)
	{
		maxLevel = newLevel > maxLevel ? newLevel : maxLevel;
	}

	public void Save ()
	{
		PlayerPrefs.SetInt ("MaxLevel", maxLevel);
	}

	public void Load ()
	{
		maxLevel = PlayerPrefs.GetInt ("MaxLevel", 1);
	}
}
