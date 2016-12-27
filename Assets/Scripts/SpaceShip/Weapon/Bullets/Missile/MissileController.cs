using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour
{
	[SerializeField] private MissileEntity _missile;
	[SerializeField] private SpriteRenderer sr;
	[SerializeField] private PolygonCollider2D polygonCollider;
	[SerializeField] private Rigidbody2D rigid;
	[SerializeField] private float errorAngle;
	[SerializeField] private float timeStart;
	[SerializeField] private float timeDown;

	private Transform _trans;
	public Vector2 direction;
	public Transform target;
	public ParticleSystem engineParticle;

	public MissileEntity missile {
		get { return _missile; }
		set {
			_missile = value; 
			sr.sprite = _missile.sprite;
		}
	}

	void OnEnable ()
	{
		polygonCollider = gameObject.AddComponent<PolygonCollider2D> ();
		polygonCollider.isTrigger = true;
		_trans = transform;
		SetDirection ();
		SetVelo ();
		timeStart = Time.time;
	}

	void OnDisable ()
	{
		_missile = null;
		Destroy (polygonCollider);
	}

	private void SetDirection ()
	{
		float angle = transform.rotation.eulerAngles.z - errorAngle;
		direction = new Vector2 (Mathf.Cos (angle * Mathf.Deg2Rad), Mathf.Sin (angle * Mathf.Deg2Rad));
		SetParticleDirection (_trans.rotation.eulerAngles.z);
	}

	private void SetVelo ()
	{
		rigid.velocity = direction * _missile.speed;
	}

	public void SetTarget (Transform target)
	{
		this.target = target;
	}

	private void SetParticleDirection (float angle)
	{
		float s = angle;
		while (s < -180 || s > 180)
			if (s < -180)
				s += 360;
			else
				s -= 360;

		if (s <= 180)
			s = -(180 + s);
		else
			s = 180 - s;
		s += 180;
		engineParticle.startRotation = Mathf.Deg2Rad * (s);
	}

	private void TurnToTarget ()
	{
		if (!target)
			return;

		Vector2 dif = target.position - _trans.position;
		float AngleTo = Mathf.Atan2 (dif.y, dif.x) * Mathf.Rad2Deg;
		float currentAngle = _trans.rotation.eulerAngles.z + errorAngle;
		float difAngle = AngleTo - currentAngle;

		while (-180 > difAngle || difAngle > 180)
			if (-180 > difAngle)
				difAngle += 360;
			else
				difAngle -= 360;
		
		float speedRot = _missile.angleRotate * Time.deltaTime * (difAngle > 0 ? -1 : 1);
		speedRot = Mathf.Abs (speedRot) > Mathf.Abs (difAngle) ? difAngle : speedRot;

		_trans.Rotate (0, 0, speedRot);

		SetDirection ();
	}

	public void Disappeal ()
	{
		MissilePooling.pool.ReturnLaser (this);
//		gameObject.SetActive (false);
	}

	void Update ()
	{
		TurnToTarget ();
		SetVelo ();
		if (Time.time - timeStart > timeDown) {
			Disappeal ();
		}
	}
}
