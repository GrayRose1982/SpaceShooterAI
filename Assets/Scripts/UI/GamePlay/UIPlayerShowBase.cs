using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIPlayerShowBase : MonoBehaviour
{
	[SerializeField] private int _current;
	[SerializeField] private int maxNum;
	[SerializeField] private Text numberTxt;

	public Slider slider;

	public int MaxNum {
		get {
			return maxNum;
		}
		set {
			maxNum = value;
			current = maxNum;
		}
	}

	public int current {
		get { return _current; }
		set {
			_current = value; 
			Show ();
		}
	}

	void OnEnable ()
	{
		
	}

	private void Show ()
	{
		numberTxt.text = _current.ToString () + " / " + maxNum.ToString ();
		slider.value = (float)_current / maxNum;
	}
}
