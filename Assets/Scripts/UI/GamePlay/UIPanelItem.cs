using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIPanelItem : MonoBehaviour
{
	[SerializeField] private Sprite uiMask;
	public Image itemImg;
	public Text numberTxt;

	[SerializeField] private bool showImgRight;

	public void SetNumber (int number)
	{
		string result = "";
		if (showImgRight)
			result = number.ToString () + " x";
		else
			result = "x " + number.ToString ();

		numberTxt.text = result;
	}

	public void SetSprite (Sprite sprite)
	{
		if (sprite != null)
			itemImg.sprite = sprite;
		else
			itemImg.sprite = uiMask;
		itemImg.SetNativeSize ();
	}

}
