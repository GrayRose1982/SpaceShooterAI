using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LaserShop : MonoBehaviour
{
	public GameObject shopSlot;
	public List<MissileWeaponEntity> MissileInShop;

	public List<MissileSlot> slotInShop;

	public LaserSlot current;

	public GridLayoutGroup grid;
	public RectTransform panelScroll;
	public float currentSize;

	void Start ()
	{
		StartCoroutine (GetWeapon ());
	}

	private IEnumerator GetWeapon ()
	{
		MissileInShop = new List<MissileWeaponEntity> ();
		while (!LoadWeaponXml.data) {
			yield return new WaitForSeconds (.1f);
		}
		while (!LoadWeaponXml.data.loadMissileDone) {
			yield return new WaitForSeconds (.1f);
		}

		MissileInShop = new List<MissileWeaponEntity> (LoadWeaponXml.data.mwData);

		for (int i = 0; i < slotInShop.Count; i++) {
			if (MissileInShop.Count <= i)
				slotInShop [i].gameObject.SetActive (false);
			else {
				if (slotInShop.Count <= i) {
					
				}
				slotInShop [i].gameObject.SetActive (true);
				slotInShop [i].weapon = new MissileWeaponEntity (MissileInShop [i]);
			}
		}

		GetCurrent ();
		SetSizeScroll (MissileInShop.Count);
	}

	private void GetCurrent ()
	{
		current.weapon = PlayerEquipment.equip.weaponController.mainWeapon;
	}

	public void SetSizeScroll (int number)
	{
		float sizeY = grid.cellSize.y * number + grid.padding.top + grid.padding.bottom + grid.spacing.y * (number - 1);

		panelScroll.sizeDelta = new Vector2 (0f, sizeY / 2);
	}
}