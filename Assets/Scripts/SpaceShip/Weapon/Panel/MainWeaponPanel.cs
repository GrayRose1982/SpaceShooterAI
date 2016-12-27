using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MainWeaponPanel : MonoBehaviour,IPointerDownHandler,IPointerEnterHandler, IPointerExitHandler
{
	public WeaponController controler;
	public bool isDoubleClick = false;

	public float lastTimeClick;
	public float doubleClickTime;

	public void OnPointerDown (PointerEventData eventData)
	{
		controler.fireMainWeapon = true;
		CheckDoubleClick ();
	}

	public void OnPointerEnter (PointerEventData eventData)
	{
		controler.fireMainWeapon = true;
		CheckDoubleClick ();
	}

	private void CheckDoubleClick ()
	{
		if (Time.time - lastTimeClick < doubleClickTime) {
			isDoubleClick = true;
		}
		lastTimeClick = Time.time;
	}

	public void OnPointerExit (PointerEventData eventData)
	{
		if (!isDoubleClick)
			controler.fireMainWeapon = false;
		else
			isDoubleClick = false;
	}
}
