using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LaserShop : MonoBehaviour
{
	public GameObject shopSlot;
	public List<LaserWeaponEntity> laserWeaponInShop;

	public List<LaserSlot> slotInShop;

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
		while (!LoadWeaponXml.data) {
			yield return new WaitForSeconds (.1f);
		}
		while (!LoadWeaponXml.data.loadLaserDone) {
			yield return new WaitForSeconds (.1f);
		}

		laserWeaponInShop = new List<LaserWeaponEntity> (LoadWeaponXml.data.lwData);

		for (int i = 0; i < slotInShop.Count; i++) {
			if (laserWeaponInShop.Count <= i)
				slotInShop [i].gameObject.SetActive (false);
			else {
				if (slotInShop.Count <= i) {
					LaserSlot s = ((GameObject)Instantiate (shopSlot, transform)).GetComponent<LaserSlot> ();

					slotInShop.Add (s);
				}
				slotInShop [i].gameObject.SetActive (true);
				slotInShop [i].weapon = new LaserWeaponEntity (laserWeaponInShop [i]);
			}
		}

		GetCurrent ();
		SetSizeScroll (laserWeaponInShop.Count);
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