using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PanelMovePlayer : MonoBehaviour,IPointerDownHandler,IPointerEnterHandler, IPointerExitHandler
{
	public bool isSelect;

	void Start ()
	{
		isSelect = false;
	}

	private void SelectPanel ()
	{
		isSelect = true;
	}

	private void DeselectPanel ()
	{
		isSelect = false;
	}

	#region IPointerDownHandler implementation

	public void OnPointerDown (PointerEventData eventData)
	{
		SelectPanel ();
	}

	#endregion

	#region IPointerEnterHandler implementation

	public void OnPointerEnter (PointerEventData eventData)
	{
		SelectPanel ();
	}

	#endregion

	#region IPointerExitHandler implementation

	public void OnPointerExit (PointerEventData eventData)
	{
		DeselectPanel ();
	}

	#endregion
}
