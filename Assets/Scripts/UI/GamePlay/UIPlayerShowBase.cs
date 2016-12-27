using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIPlayerShowBase : MonoBehaviour
{
	[SerializeField] private int _current;
	[SerializeField] private float pointPerNode;
	[SerializeField] private  int maxNum;

	public Image[] showNodes;

	public int MaxNum {
		get {
			return maxNum;
		}
		set {
			maxNum = value;
			CalPercentPerNode ();
			current = maxNum;
		}
	}

	public int current {
		get { return _current; }
		set {
			_current = value; 
			ChangeSprite ();
		}
	}

	void OnEnable ()
	{
		CalPercentPerNode ();
	}

	//	public void Lose (int number)
	//	{
	//		if (number > current)
	//			current = 0;
	//		else
	//			current -= number;
	//	}

	public void CalPercentPerNode ()
	{
		pointPerNode = maxNum / showNodes.Length;
	}

	private void ChangeSprite ()
	{
		int round = (int)(_current / pointPerNode);
		Color now = Color.white;
		for (int i = 0; i < round; i++) {
			showNodes [i].color = now;
		}

		if (round >= showNodes.Length)
			return;

		float last = (_current - (pointPerNode * round)) / pointPerNode;
		now.a = last;

		if (round > 0)
			showNodes [round].color = now;
//		Debug.Log (now + " " + showNodes [round].color);
		now.a = 0;
		for (int i = round < 0 ? 0 : round; i < showNodes.Length; i++) {
			showNodes [i].color = now;
		}
	}
}
