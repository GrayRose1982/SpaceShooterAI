using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MissileWeaponPanel : MonoBehaviour,IPointerDownHandler,IPointerEnterHandler, IPointerExitHandler
{
	public WeaponController controler;
	public bool isDoubleClick = false;

	public float lastTimeClick;
	public float doubleClickTime;

	public void OnPointerDown (PointerEventData eventData)
	{
		controler.fireMissileWeapon = true;
		CheckDoubleClick ();
	}

	public void OnPointerEnter (PointerEventData eventData)
	{
		controler.fireMissileWeapon = true;
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
			controler.fireMissileWeapon = false;
		else
			isDoubleClick = false;
	}
}
