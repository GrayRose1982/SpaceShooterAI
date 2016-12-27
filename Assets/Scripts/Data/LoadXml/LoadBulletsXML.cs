using UnityEngine;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

public class LoadBulletsXML:MonoBehaviour
{
	public static LoadBulletsXML data;

	public Sprite[] spriteMissiles;
	public Sprite[] spriteLaser;

	public List<MissileEntity> mStorage;
	public List<LaserEntity> lStorage;

	public bool isLoadDone;

	void Start ()
	{
		data = this;

		mStorage = new List<MissileEntity> ();
		lStorage = new List<LaserEntity> ();

		StartCoroutine (LoadMissileData ());
		StartCoroutine (LoadLaserData ());
	}

	#region Missile data

	//Load item to datalist
	private	IEnumerator LoadMissileData ()
	{
		isLoadDone = false;
		TextAsset xml = Resources.Load<TextAsset> ("Data/Missile");
		yield return xml;
		if (xml == null) {
			Debug.Log ("bug here");
		}
		XmlDocument doc = new XmlDocument ();
		doc.PreserveWhitespace = false;

		doc.LoadXml (xml.text);

		LoadListMissile (doc.SelectNodes ("dataroot/Missile"));
	}


	// Converts an XmlNodeList into item objects and add to datalist
	private void LoadListMissile (XmlNodeList nodes)
	{
		MissileEntity item;
		foreach (XmlNode node in nodes) {
			item = GetInforMissile (node);
			mStorage.Add (item);
		}
		isLoadDone = true;
	}

	//	Set information for item
	private MissileEntity GetInforMissile (XmlNode info)
	{
		MissileEntity item = new MissileEntity ();
		item.name = info.SelectSingleNode ("Name").InnerText;
		item.id = "M" + info.SelectSingleNode ("ID").InnerText;
		item.speed = float.Parse (info.SelectSingleNode ("Speed").InnerText);
		item.angleRotate = int.Parse (info.SelectSingleNode ("AngleRotate").InnerText);

		item.sprite = spriteMissiles [mStorage.Count % spriteMissiles.Length];
//		string spriteName = item.name.Replace (" ", "_");
//		item.sprite = Resources.Load<Sprite> ("Sprites/" + spriteName);

		return item;
	}

	#endregion

	#region Laser data

	//Load item to datalist
	private	IEnumerator LoadLaserData ()
	{
		isLoadDone = false;
		TextAsset xml = Resources.Load<TextAsset> ("Data/Laser");
		yield return xml;
		if (xml == null) {
			Debug.Log ("bug here");
		}
		XmlDocument doc = new XmlDocument ();
		doc.PreserveWhitespace = false;

		doc.LoadXml (xml.text);

		LoadListLaser (doc.SelectNodes ("dataroot/Laser"));
	}


	// Converts an XmlNodeList into item objects and add to datalist
	private void LoadListLaser (XmlNodeList nodes)
	{
		LaserEntity item;
		foreach (XmlNode node in nodes) {
			item = GetInforLaser (node);
			lStorage.Add (item);
		}
		isLoadDone = true;
	}

	private LaserEntity GetInforLaser (XmlNode info)
	{
		LaserEntity item = new LaserEntity ();
		item.name = info.SelectSingleNode ("Name").InnerText;
		item.id = "L" + info.SelectSingleNode ("ID").InnerText;
		item.speed = float.Parse (info.SelectSingleNode ("Speed").InnerText);

		item.sprite = spriteLaser [lStorage.Count % spriteLaser.Length];

//		string spriteName = item.name.Replace (" ", "_");
//		item.sprite = Resources.Load<Sprite> ("Sprites/" + spriteName);

		return item;
	}

	#endregion
}
