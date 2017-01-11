using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour
{
	[SerializeField] private LaserWeaponEntity _mainWeapon;
	[SerializeField] private MissileWeaponEntity _secondWeapon;

	public Transform[] mainBarrels;
	public Transform[] secondBarrels;

	public float timerMainWeapon;
	public float timerSecondWeapon;

	public bool fireMainWeapon;
	public bool fireMissileWeapon;

	public List<Transform> targets;
	//	[SerializeField] private Rigidbody2D rigid;
	[SerializeField] private Transform crossHair;
	[SerializeField] private UIPlayerController ui;

	public LaserWeaponEntity mainWeapon {
		get {
			return _mainWeapon;
		}
		set {
			DestroyObject (_mainWeapon.barrels);
			_mainWeapon = value;
			_mainWeapon.laser.damage = _mainWeapon.damage;
			_mainWeapon.laser.armorBreak = _mainWeapon.armorBreak;

			_mainWeapon.barrels = (GameObject)Instantiate (_mainWeapon.barrels, transform);
			ChangeGunBarrelMainWeapon (_mainWeapon.barrels.transform);

			_mainWeapon.barrels.transform.localPosition = Vector3.zero;
			_mainWeapon.barrels.transform.localRotation = Quaternion.identity;

			SetUINewItem (ui.laserShow, _mainWeapon.laser.sprite, _mainWeapon.number);
		}
	}

	public MissileWeaponEntity secondWeapon {
		get {
			return _secondWeapon;
		}
		set {
			_secondWeapon = value;
			_secondWeapon.missile.damage = _secondWeapon.damage;
			_secondWeapon.missile.armorBreak = _secondWeapon.armorBreak;

			SetUINewItem (ui.missileShow, _secondWeapon.missile.sprite, _secondWeapon.number);
		}
	}

	void OnEnable ()
	{

		targets = new List<Transform> ();
		timerMainWeapon = Time.time;
		timerSecondWeapon = Time.time;

		_mainWeapon.laser.damage = _mainWeapon.damage;
		_mainWeapon.laser.armorBreak = _mainWeapon.armorBreak;
		_secondWeapon.missile.damage = _secondWeapon.damage;
		_secondWeapon.missile.armorBreak = _secondWeapon.armorBreak;

		if (!ui)
			return;

		SetUINewItem (ui.laserShow, _mainWeapon.laser.sprite, _mainWeapon.number);
		SetUINewItem (ui.missileShow, _secondWeapon.missile.sprite, _secondWeapon.number);
	}

	void Update ()
	{
		ControlFire ();
		SetCrossHair ();
	}

	public void ControlFire ()
	{
		if (fireMainWeapon)
			FireMainWeapon ();
//		if (fireMissileWeapon)
//			FireMissileWeapon ();
	}

	public void NextTarget ()
	{
		if (targets.Count <= 1)
			return;

		Transform first = targets [0];
		targets.RemoveAt (0);
		targets.Add (first);
	}

	private void ChangeGunBarrelMainWeapon (Transform parentGunBarrels)
	{
		mainBarrels = new Transform[parentGunBarrels.childCount];

		for (int i = 0; i < mainBarrels.Length; i++) {
			mainBarrels [i] = parentGunBarrels.GetChild (i);
		}
	}

	private void ChangeGunBarrelSecondWeapon (Transform parentGunBarrels)
	{
		secondBarrels = new Transform[parentGunBarrels.childCount];

		for (int i = 0; i < mainBarrels.Length; i++) {
			secondBarrels [i] = parentGunBarrels.GetChild (i);
		}
	}

	private void SetCrossHair ()
	{
		if (targets.Count > 0)
			crossHair.transform.position = targets [0].position;
	}

	private void FireMainWeapon ()
	{
		if (!LaserPooling.pool || timerMainWeapon >= Time.time || _mainWeapon.laser == null)
			return;
		
		timerMainWeapon = Time.time + _mainWeapon.timePerShoot;
		foreach (Transform t in mainBarrels) {
			LaserController l = LaserPooling.pool.GetLaser ();
			if (l) {
				l.transform.position = t.position;
				l.transform.rotation = t.rotation;
				//TODO: Change information of bullet like: name, id, damage...

				l.laser = new LaserEntity (_mainWeapon.laser);
				l.gameObject.layer = LayerMask.NameToLayer ("Player");
				l.gameObject.SetActive (true);

				if (ui)
					SetUIFire (ui.laserShow, _mainWeapon.number--);

				if (_mainWeapon.number <= 0) {
					_mainWeapon = LoadWeaponXml.data.lwData [0];
				}
			}
		}
		
	}

	private void FireMissileWeapon ()
	{
		if (!MissilePooling.pool)
			Debug.LogWarning ("Dont have pooling");
		if (!MissilePooling.pool || timerSecondWeapon >= Time.time || _secondWeapon.missile == null)
			return;

		timerSecondWeapon = Time.time + _secondWeapon.timePerShoot;
		foreach (Transform t in mainBarrels) {
			MissileController m = MissilePooling.pool.GetMissile ();
			if (m) {
				m.transform.position = t.position;
				m.transform.rotation = t.rotation;
				//TODO: Change information of bullet like: name, id, damage...
				m.missile = new MissileEntity (_secondWeapon.missile);
				m.gameObject.layer = LayerMask.NameToLayer ("Player");
				m.SetTarget (targets.Count > 0 ? targets [0] : null);
				m.gameObject.SetActive (true);

				if (ui)
					SetUIFire (ui.missileShow, _secondWeapon.number--);

				if (_secondWeapon.number <= 0) {
					_secondWeapon = LoadWeaponXml.data.mwData [0];
				}
			}
		}
	}

	private void SetUIFire (UIPanelItem item, int number)
	{
		item.SetNumber (number);
	}

	private void SetUINewItem (UIPanelItem item, Sprite sprite, int number)
	{
		item.SetNumber (number);
		item.SetSprite (sprite);
	}

	void OnTriggerEnter2D (Collider2D hit)
	{
		if (hit.CompareTag ("Character"))
			targets.Add (hit.transform);

		if (!crossHair.gameObject.activeSelf)
			crossHair.gameObject.SetActive (true);
		
	}

	void OnTriggerExit2D (Collider2D hit)
	{
		if (hit.CompareTag ("Character"))
			targets.Remove (hit.transform);

		if (targets.Count == 0)
			crossHair.gameObject.SetActive (false);
	}
}
