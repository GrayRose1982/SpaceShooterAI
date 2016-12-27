using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIWeaponController : MonoBehaviour
{
	[SerializeField] private LaserWeaponEntity _mainWeapon;
	[SerializeField] private MissileWeaponEntity _secondWeapon;

	public Transform[] mainBarrels;
	public Transform[] secondBarrels;

	public float timerMainWeapon;
	public float timerSecondWeapon;

	public TypeShipShooter typeWeapon = TypeShipShooter.Laser;

	public bool fireMainWeapon;
	public bool fireMissileWeapon;

	public List<Transform> targets;
	//	[SerializeField] private Rigidbody2D rigid;

	public LaserWeaponEntity mainWeapon {
		get {
			return _mainWeapon;
		}
		set {
			_mainWeapon = value;

			if (_mainWeapon.laser == null)
				_mainWeapon.laser = new LaserEntity ();
			_mainWeapon.laser.damage = _mainWeapon.damage;
			_mainWeapon.laser.armorBreak = _mainWeapon.armorBreak;
		}
	}

	public MissileWeaponEntity secondWeapon {
		get {
			return _secondWeapon;
		}
		set {
			_secondWeapon = value;

			if (_secondWeapon.missile == null)
				_secondWeapon.missile = new MissileEntity ();
			_secondWeapon.missile.damage = _secondWeapon.damage;
			_secondWeapon.missile.armorBreak = _secondWeapon.armorBreak;
		}
	}

	void OnEnable ()
	{
		targets = new List<Transform> ();
		timerMainWeapon = Time.time;
		timerSecondWeapon = Time.time;

		fireMainWeapon = false;
		fireMissileWeapon = false;

//		_mainWeapon.laser.damage = _mainWeapon.damage;
//		_mainWeapon.laser.armorBreak = _mainWeapon.armorBreak;
//		_secondWeapon.missile.damage = _secondWeapon.damage;
//		_secondWeapon.missile.armorBreak = _secondWeapon.armorBreak;
	}

	void Update ()
	{
		ControlFire ();
	}

	public void ControlFire ()
	{
		switch (typeWeapon) {
		case TypeShipShooter.Laser:
			if (fireMainWeapon && _mainWeapon != null)
				FireMainWeapon ();
			break;
		case TypeShipShooter.Missile:
			if (fireMissileWeapon && _secondWeapon != null)
				FireMissileWeapon ();
			break;
		default:
			break;
		}
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
				l.gameObject.layer = LayerMask.NameToLayer ("Enemy");
				l.laser = new LaserEntity (_mainWeapon.laser);
				l.gameObject.SetActive (true);
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
				m.gameObject.layer = LayerMask.NameToLayer ("Enemy");
				m.SetTarget (targets.Count > 0 ? targets [0] : null);
				m.gameObject.SetActive (true);
			}
		}
	}

	void OnTriggerEnter2D (Collider2D hit)
	{
		if (hit.CompareTag ("Player"))
			targets.Add (hit.transform);

		fireMainWeapon = true;
		fireMissileWeapon = true;
	}

	void OnTriggerExit2D (Collider2D hit)
	{
		if (hit.CompareTag ("Player"))
			targets.Remove (hit.transform);

		if (targets.Count == 0) {
			fireMainWeapon = false;
			fireMissileWeapon = false;
		}
	}
}
