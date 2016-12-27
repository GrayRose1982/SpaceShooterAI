using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MoveControllerNet : NetworkBehaviour
{
	public PanelMovePlayer left;
	public PanelMovePlayer right;

	public float speedMove = 3f;
	public float maxSpeed = 10f;
	public float speedRotate = 50f;

	public ParticleSystem engineParticle;

	public Vector2 direction;

	private Transform _trans;
	[SerializeField] private float errorAngle;
	[SerializeField] private Rigidbody2D rigid;

	void Start ()
	{
		if (!isLocalPlayer)
			Destroy (this);

		_trans = transform;
		SetDirection ();
	}

	void Update ()
	{
		if (left.isSelect && !right.isSelect) {
			_trans.Rotate (0, 0, speedRotate * Time.deltaTime);
			SetDirection ();
		} else if (!left.isSelect && right.isSelect) {
			_trans.Rotate (0, 0, -speedRotate * Time.deltaTime);
			SetDirection ();
		}

		MoveToward ();
	}

	private void SetDirection ()
	{
		float angle = _trans.rotation.eulerAngles.z - errorAngle;
		direction = new Vector2 (Mathf.Cos (angle * Mathf.Deg2Rad), Mathf.Sin (angle * Mathf.Deg2Rad));
		SetParticleDirection (angle - errorAngle);
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

	private void MoveToward ()
	{
		//		Vector2 current = _trans.position;
		//		_trans.position = Vector2.MoveTowards (current, current + direction.normalized, Time.deltaTime * speedMove);

		rigid.velocity = direction * speedMove;
	}
}
