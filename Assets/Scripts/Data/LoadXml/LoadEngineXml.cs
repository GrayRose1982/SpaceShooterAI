using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class LoadEngineXml : MonoBehaviour
{
	public static LoadEngineXml data;

	//	public Sprite[] spriteCharacters;
	public List<EngineEntity> eData;

	public bool isLoadDone;

	void Start ()
	{
		data = this;

		eData = new List<EngineEntity> ();

		StartCoroutine (LoadEngineData ());
	}

	//Load item to datalist
	private	IEnumerator LoadEngineData ()
	{
		isLoadDone = false;
		TextAsset xml = Resources.Load<TextAsset> ("Data/Engine");
		yield return xml;
		if (xml == null) {
			Debug.Log ("bug here");
		}
		XmlDocument doc = new XmlDocument ();
		doc.PreserveWhitespace = false;

		doc.LoadXml (xml.text);

		LoadListEngine (doc.SelectNodes ("dataroot/Engine"));
	}


	// Converts an XmlNodeList into item objects and add to datalist
	private void LoadListEngine (XmlNodeList nodes)
	{
		EngineEntity item;
		foreach (XmlNode node in nodes) {
			item = GetInfor (node);
			eData.Add (item);
		}
		isLoadDone = true;
	}

	//	Set information for item
	private EngineEntity GetInfor (XmlNode info)
	{
		EngineEntity item = new EngineEntity ();
		item.name = info.SelectSingleNode ("Name").InnerText;
		item.id = "C" + info.SelectSingleNode ("ID").InnerText;
		item.maxSpeed = float.Parse (info.SelectSingleNode ("MaxSpeed").InnerText);
		item.minSpeed = float.Parse (info.SelectSingleNode ("MinSpeed").InnerText);
		item.angleRotate = float.Parse (info.SelectSingleNode ("AngleRotate").InnerText);

//		item.sprite = spriteCharacters [EnigneEntityData.Count % spriteCharacters.Length];
		//		string spriteName = item.name.Replace (" ", "_");
		//		item.sprite = Resources.Load<Sprite> ("Sprites/" + spriteName);

		return item;
	}

	public EngineEntity GetCharacter (int index)
	{
		EngineEntity c = new EngineEntity ();
		if (index < eData.Count)
			c = new EngineEntity (eData [index]);

		return c;
	}
}
