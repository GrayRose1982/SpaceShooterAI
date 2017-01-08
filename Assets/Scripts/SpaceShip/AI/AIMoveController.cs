using UnityEngine;
using System.Collections;

public class AIMoveController : MonoBehaviour
{
	public float speedMove = 3f;
	public float minSpeed = 1f;
	public float maxSpeed = 6f;
	public float speedRotate = 50f;

	public ParticleSystem engineParticle;

	public Vector2 direction;

	private Transform _trans;
	private float sizeDeltaMove = .5f;

	[SerializeField] private TypeAIMove typeMove;
	[SerializeField] private Vector2 posMoveWhenFree;

	[SerializeField] private Transform target;
	[SerializeField] private float errorAngle;
	[SerializeField] private Rigidbody2D rigid;

	void OnEnable ()
	{
//		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Start ()
	{
		if (!rigid)
			rigid = GetComponent<Rigidbody2D> ();
		
		if (!target)
			GetNewPosToMove ();
		
		_trans = transform;
		SetDirection ();
	}

	void Update ()
	{
		switch (typeMove) {
		case TypeAIMove.Free:
			RotateToTarget ();
			break;
		default:
			break;
		}

		MoveToward ();

	}

	private void RotateToTarget ()
	{
		Vector2 dif;

		if (!target) {
			dif = posMoveWhenFree - (Vector2)_trans.position;
			if (dif.magnitude <= sizeDeltaMove)
				GetNewPosToMove ();
		} else
			dif = target.position - _trans.position;

		float AngleTo = Mathf.Atan2 (dif.y, dif.x) * Mathf.Rad2Deg;
		float currentAngle = _trans.rotation.eulerAngles.z + errorAngle;
		float difAngle = AngleTo - currentAngle;

		while (-180 > difAngle || difAngle > 180)
			if (-180 > difAngle)
				difAngle += 360;
			else
				difAngle -= 360;

		float speedRot;

		if (difAngle == 180 || difAngle == -180)
			speedRot = speedRotate * Time.deltaTime;
		else
			speedRot = speedRotate * Time.deltaTime * (difAngle > 0 ? -1 : 1);
		

		if (difAngle != 0)
			speedRot = Mathf.Abs (speedRot) > Mathf.Abs (difAngle) ? difAngle : speedRot;
		
		_trans.Rotate (0, 0, speedRot);
	}

	private void SetDirection ()
	{
		float angle = _trans.rotation.eulerAngles.z - errorAngle;
		direction = new Vector2 (Mathf.Cos (angle * Mathf.Deg2Rad), Mathf.Sin (angle * Mathf.Deg2Rad));

		if (engineParticle)
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
		SetDirection ();
		rigid.velocity = direction * speedMove;
	}

	private void GetNewPosToMove ()
	{
		if (!GenerateForFree.generateMap)
			return;

		posMoveWhenFree = GenerateForFree.generateMap.GetRandomSpace ();
	}

	public void GetNewTarget (Transform target)
	{
		this.target = target;
	}

	public void TargetOut (Transform target)
	{
//		if (this.target == target)
		target = null;
	}
}
