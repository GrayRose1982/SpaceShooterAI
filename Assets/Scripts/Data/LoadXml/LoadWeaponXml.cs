using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class LoadWeaponXml : MonoBehaviour
{
	public static LoadWeaponXml data;

	public List<LaserWeaponEntity> lwData;
	public List<MissileWeaponEntity> mwData;

	public GameObject[] gunBarrels;

	private bool isLoadWeaponLaserDone;
	private bool isLoadWeaponMissileDone;

	public bool loadLaserDone;
	public bool loadMissileDone;

	void Start ()
	{
		data = this;
		loadLaserDone = false;
		loadMissileDone = false;
		lwData = new List<LaserWeaponEntity> ();
		mwData = new List<MissileWeaponEntity> ();

		StartCoroutine (LoadLaserWeaponData ());
		StartCoroutine (LoadMissileWeaponData ());

		StartCoroutine (UpdateLaserBullet ());
	}

	#region laser weapon

	//Load item to datalist
	private	IEnumerator LoadLaserWeaponData ()
	{
		isLoadWeaponLaserDone = false;
		TextAsset xml = Resources.Load<TextAsset> ("Data/LaserWeapon");
		yield return xml;
		if (xml == null) {
			Debug.LogWarning ("U dont have xml data here");
		}
		XmlDocument doc = new XmlDocument ();
		doc.PreserveWhitespace = false;

		doc.LoadXml (xml.text);

		LoadListLaserWeapon (doc.SelectNodes ("dataroot/LaserWeapon"));
	}


	// Converts an XmlNodeList into item objects and add to datalist
	private void LoadListLaserWeapon (XmlNodeList nodes)
	{
		LaserWeaponEntity item;
		foreach (XmlNode node in nodes) {
			item = GetInforLaserWeapon (node);
			lwData.Add (item);
		}
		isLoadWeaponLaserDone = true;
	}

	//	Set information for item
	private LaserWeaponEntity GetInforLaserWeapon (XmlNode info)
	{
		LaserWeaponEntity item = new LaserWeaponEntity ();
		item.bulletID = info.SelectSingleNode ("BulletID").InnerText;
		item.id = "LW" + info.SelectSingleNode ("ID").InnerText;
		item.damage = int.Parse (info.SelectSingleNode ("Damage").InnerText);
		item.armorBreak = int.Parse (info.SelectSingleNode ("ArmorBreak").InnerText);
		item.timePerShoot = float.Parse (info.SelectSingleNode ("TimePerShoot").InnerText);
		string gunBarrel = info.SelectSingleNode ("GunBarrels").InnerText;

		foreach (GameObject gun in gunBarrels)
			if (gun.name.CompareTo (gunBarrel) == 0) {
				item.barrels = gun;
				break;
			}
		
		return item;
	}



	#endregion

	#region Missile weapon

	private	IEnumerator LoadMissileWeaponData ()
	{
		isLoadWeaponMissileDone = false;
		TextAsset xml = Resources.Load<TextAsset> ("Data/MissileWeapon");
		yield return xml;
		if (xml == null) {
			Debug.Log ("bug here");
		}
		XmlDocument doc = new XmlDocument ();
		doc.PreserveWhitespace = false;

		doc.LoadXml (xml.text);

		LoadListMissileWeapon (doc.SelectNodes ("dataroot/MissileWeapon"));
	}


	// Converts an XmlNodeList into item objects and add to datalist
	private void LoadListMissileWeapon (XmlNodeList nodes)
	{
		MissileWeaponEntity item;
		foreach (XmlNode node in nodes) {
			item = GetInfor (node);
			mwData.Add (item);
		}
		isLoadWeaponMissileDone = true;
	}
	
	//	Set information for item
	private MissileWeaponEntity GetInfor (XmlNode info)
	{
		MissileWeaponEntity item = new MissileWeaponEntity ();

		item.bulletID = info.SelectSingleNode ("BulletID").InnerText;
		item.id = "MW" + info.SelectSingleNode ("ID").InnerText;
		item.damage = int.Parse (info.SelectSingleNode ("Damage").InnerText);
		item.armorBreak = int.Parse (info.SelectSingleNode ("ArmorBreak").InnerText);
		item.timePerShoot = float.Parse (info.SelectSingleNode ("TimePerShoot").InnerText);

		return item;
	}

	#endregion

	private IEnumerator UpdateLaserBullet ()
	{
		while (!LoadBulletsXML.data)
			yield return new WaitForSeconds (.2f);

		while (!LoadBulletsXML.data.isLoadDone)
			yield return new WaitForSeconds (.2f);

		foreach (LaserWeaponEntity lw in lwData) {
			int index = int.Parse (lw.bulletID.Substring (1));
			lw.laser = LoadBulletsXML.data.lStorage [index];
		}
		loadLaserDone = true;


		while (!LoadBulletsXML.data)
			yield return new WaitForSeconds (.2f);

		while (!LoadBulletsXML.data.isLoadDone)
			yield return new WaitForSeconds (.2f);

		foreach (MissileWeaponEntity mw in mwData) {
			int index = int.Parse (mw.bulletID.Substring (1));
			mw.missile = LoadBulletsXML.data.mStorage [index];
		}
		loadMissileDone = true;
	}

	public MissileWeaponEntity GetMissileWeapon (int index)
	{
		MissileWeaponEntity c = new MissileWeaponEntity ();
		if (index < mwData.Count)
			c = new MissileWeaponEntity (mwData [index]);

		return c;
	}

	public LaserWeaponEntity GetLaserWeapon (int index)
	{
		LaserWeaponEntity c = new LaserWeaponEntity ();
		if (index < lwData.Count)
			c = new LaserWeaponEntity (lwData [index]);

		return c;
	}
}
