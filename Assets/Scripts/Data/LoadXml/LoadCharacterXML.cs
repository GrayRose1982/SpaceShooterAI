using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class LoadCharacterXML : MonoBehaviour
{
	public static LoadCharacterXML data;

	public Sprite[] spriteCharacters;
	public List<BaseCharacter> baseCharacterData;

	public bool isLoadDone;

	void Start ()
	{
		data = this;

		baseCharacterData = new List<BaseCharacter> ();

		StartCoroutine (LoadMissileData ());
	}

	//Load item to datalist
	private	IEnumerator LoadMissileData ()
	{
		isLoadDone = false;
		TextAsset xml = Resources.Load<TextAsset> ("Data/Character");
		yield return xml;
		if (xml == null) {
			Debug.Log ("bug here");
		}
		XmlDocument doc = new XmlDocument ();
		doc.PreserveWhitespace = false;

		doc.LoadXml (xml.text);

		LoadListMissile (doc.SelectNodes ("dataroot/Character"));
	}


	// Converts an XmlNodeList into item objects and add to datalist
	private void LoadListMissile (XmlNodeList nodes)
	{
		BaseCharacter item;
		foreach (XmlNode node in nodes) {
			item = GetInfor (node);
			baseCharacterData.Add (item);
		}
		isLoadDone = true;
	}

	//	Set information for item
	private BaseCharacter GetInfor (XmlNode info)
	{
		BaseCharacter item = new BaseCharacter ();
		item.name = info.SelectSingleNode ("Name").InnerText;
		item.id = "C" + info.SelectSingleNode ("ID").InnerText;
		item.hp = int.Parse (info.SelectSingleNode ("Hp").InnerText);
		item.armor = int.Parse (info.SelectSingleNode ("Armor").InnerText);

		item.sprite = spriteCharacters [baseCharacterData.Count % spriteCharacters.Length];

//		string spriteName = item.name.Replace (" ", "_");
//		item.sprite = Resources.Load<Sprite> ("Sprites/" + spriteName);

		return item;
	}

	public BaseCharacter GetCharacter (int index)
	{
		BaseCharacter c = new BaseCharacter ();
		if (index < baseCharacterData.Count)
			c = new BaseCharacter (baseCharacterData [index]);
		
		return c;
	}
}
