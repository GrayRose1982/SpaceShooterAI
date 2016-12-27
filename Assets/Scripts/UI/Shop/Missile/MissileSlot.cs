using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MissileSlot : MonoBehaviour
{
	[SerializeField] private MissileWeaponEntity _weapon;

	public MissileWeaponEntity weapon {
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
	public Transform arFill;

	private void Fill (float max, float current, Transform fill)
	{
		float fillx = current / max;
		fill.localScale = new Vector3 (fillx, 1f, 1f);
	}

	private void FillInverse (float max, float current, Transform fill)
	{
		float fillx = max / current;

		fill.localScale = new Vector3 (fillx, 1f, 1f);
	}

	private void ChangeDescription ()
	{
		mainImg.sprite = _weapon.missile.sprite;
		mainImg.SetNativeSize ();
		nameTxt.text = _weapon.missile.name;

		Fill (ConstNumber.MissileEnergyDamageMax, _weapon.damage, edFill);
		Fill (ConstNumber.MissileShieldDamageMax, _weapon.armorBreak, sdFill);
		Fill (ConstNumber.MissileSpeedMax, _weapon.missile.speed, speedFill);
		Fill (ConstNumber.MissileAngleRotateMax, _weapon.missile.angleRotate, arFill);
		FillInverse (ConstNumber.MissileTimePerShootMax, _weapon.timePerShoot, tpfFill);
	}
}
