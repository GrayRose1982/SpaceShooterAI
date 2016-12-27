using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDroppedPooling : MonoBehaviour
{
	public static ItemDroppedPooling pool;

	public GameObject itemDropped;
	public bool loadDone;

	public List<ItemDropped> items;
	public List<ItemDropped> itemsOutSide;

	void Start ()
	{
		itemsOutSide = new List<ItemDropped> ();
		items = new List<ItemDropped> ();
		pool = this;

		StartCoroutine (SpawnLaser (20));
	}

	public IEnumerator SpawnLaser (int number)
	{
		loadDone = false;
		for (int i = 0; i < number; i++) {
			GameObject go = (GameObject)Instantiate (itemDropped, transform);
			yield return new WaitForSeconds (0.005f);
			ItemDropped drop = go.GetComponent<ItemDropped> ();
			if (drop)
				ReturnDropBox (drop);
		}
		loadDone = true;
	}

	//	public ItemDropped GetDropBox ()
	//	{
	//		ItemDropped result = null;
	//
	//		if (items.Count > 0) {
	//			result = items [0];
	//			result.transform.SetParent (null);
	//			items.RemoveAt (0);
	//		} else {
	//			StartCoroutine (SpawnLaser (3));
	//		}
	//
	//		return result;
	//	}
	public void GetDropBox (Vector2 pos)
	{
		ItemDropped result = null;

		if (items.Count > 0) {
			result = items [0];
			result.transform.SetParent (null);
			result.gameObject.SetActive (true);
			result.transform.position = pos;

			items.RemoveAt (0);
			itemsOutSide.Add (result);

			TypeItemDrop typeDrop = (TypeItemDrop)Random.Range (0, 3);
			int r = 0;
			switch (typeDrop) {
			case TypeItemDrop.Character:
				r = Random.Range (-1, LoadCharacterXML.data.baseCharacterData.Count);
				result.baseCharacter = new BaseCharacter (LoadCharacterXML.data.baseCharacterData [r]);
				break;
			case TypeItemDrop.LaserWeapon:
				r = Random.Range (-1, LoadWeaponXml.data.lwData.Count);
				result.lwEntity = new LaserWeaponEntity (LoadWeaponXml.data.lwData [r]);
				break;
			case TypeItemDrop.MissileWeapon:
				r = Random.Range (-1, LoadWeaponXml.data.mwData.Count);
				result.mwEntity = new MissileWeaponEntity (LoadWeaponXml.data.mwData [r]);
				break;
			default:
				break;
			}

		} else
			StartCoroutine (SpawnLaser (3));
	}

	public void ReturnDropBox (ItemDropped l)
	{
		l.transform.SetParent (transform);
		l.gameObject.SetActive (false);
		items.Add (l);
		itemsOutSide.Remove (l);
	}
}
