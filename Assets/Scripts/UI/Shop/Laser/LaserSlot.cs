using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LaserSlot : MonoBehaviour
{
	[SerializeField] private LaserWeaponEntity _weapon;

	public LaserWeaponEntity weapon {
		get { return _weapon; }
		set {
			_weapon = value;
			ChangeDescription ();
		}
	}

	public Image mainImg;
	public Text nameTxt;
	public Text energyDamageTxt;
	public Text shieldDamageTxt;
	public Text timePerShootTxt;
	public Text speedTxt;
	public Text angleRotateTxt;

	public Transform edFill;
	public Transform sdFill;
	public Transform tpfFill;
	public Transform speedFill;

	private void Fill (float max, float current, Transform fill)
	{
		float fillx = current / max;
		fill.localScale = new Vector3 (fillx, 1f, 1f);
	}

	private void FillInverse (float max, float current, Transform fill)
	{
		float fillx = current / max;
		fillx = 1 / fillx;
		fill.localScale = new Vector3 (fillx, 1f, 1f);
	}

	private void ChangeDescription ()
	{
		mainImg.sprite = _weapon.laser.sprite;
		mainImg.SetNativeSize ();
		nameTxt.text = _weapon.laser.name;

		Fill (ConstNumber.LaserEnergyDamageMax, _weapon.damage, edFill);
		Fill (ConstNumber.LaserShieldDamageMax, _weapon.armorBreak, sdFill);
		Fill (ConstNumber.LaserSpeedMax, _weapon.laser.speed, speedFill);
		Fill (ConstNumber.LaserTimePerShootMax, _weapon.timePerShoot, tpfFill);
	}
}
