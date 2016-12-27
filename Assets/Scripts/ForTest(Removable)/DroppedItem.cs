using UnityEngine;
using System.Collections;

public class DroppedItem : MonoBehaviour
{
	public ItemDropped[] items;

	void Start ()
	{
		StartCoroutine (setUp ());
	}

	private IEnumerator setUp ()
	{
		yield return new WaitForSeconds (.5f);

		while (!LoadWeaponXml.data || !LoadCharacterXML.data)
			yield return new WaitForSeconds (.1f);

		while (!LoadWeaponXml.data.loadLaserDone ||
		       !LoadWeaponXml.data.loadMissileDone ||
		       !LoadCharacterXML.data.isLoadDone)
			yield return new WaitForSeconds (.1f);


		items [0].baseCharacter = LoadCharacterXML.data.baseCharacterData [2];
		items [1].mwEntity = LoadWeaponXml.data.mwData [1];
		items [2].lwEntity = LoadWeaponXml.data.lwData [1];
	}
}
