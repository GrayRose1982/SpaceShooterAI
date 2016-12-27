using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpeedPanel :MonoBehaviour,IPointerEnterHandler,IPointerClickHandler, IDragHandler, IPointerExitHandler, IPointerUpHandler
{
	[SerializeField] private Image speedImg;
	[SerializeField] private Sprite increaseSprite;
	[SerializeField] private Sprite decreaseSprite;
	//	private Vector2 previousPos;
	//	private Vector2 posTo;

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		SetSpeedUI (eventData);
	}

	#endregion

	#region IPointerEnterHandler implementation

	public void OnPointerEnter (PointerEventData eventData)
	{
		SetSpeedUI (eventData);
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		Drag (eventData);
	}

	#endregion

	#region IPointerExitHandler implementation

	public void OnPointerExit (PointerEventData eventData)
	{
		OnExitOrUp ();
	}

	#endregion

	#region IPointerUpHandler implementation

	public void OnPointerUp (PointerEventData eventData)
	{
		OnExitOrUp ();
	}

	#endregion

	private void SetSpeedUI (PointerEventData eventData)
	{
		speedImg.gameObject.SetActive (true);
		Vector2 pos = speedImg.transform.position;
		Vector2 pointer = Camera.main.ScreenToWorldPoint (eventData.position);
		pos.y = pointer.y;
		speedImg.transform.position = pos;
	}

	private void Drag (PointerEventData eventData)
	{
		Vector2 pos = speedImg.transform.position;
		Vector2 pointer = Camera.main.ScreenToWorldPoint (eventData.position);
		if (pos.y > pointer.y)
			speedImg.sprite = decreaseSprite;
		else
			speedImg.sprite = increaseSprite;
		pos.y = pointer.y;
		speedImg.transform.position = pos;
	}

	private void OnExitOrUp ()
	{
		speedImg.gameObject.SetActive (false);
	}


}
