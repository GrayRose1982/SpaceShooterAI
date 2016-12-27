using UnityEngine;
using System.Collections;

public class OutOfMap : MonoBehaviour
{
	void OnTriggerEnter2D (Collider2D hit)
	{
		if (hit.CompareTag ("Laser")) {
			hit.GetComponent<LaserController> ().Disappeal ();
		}
	}
}
