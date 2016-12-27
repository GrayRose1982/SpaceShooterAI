using UnityEngine;
using System.Collections;

public enum TypeItemDrop
{
	None = 0,
	Character = 1,
	LaserWeapon = 2,
	MissileWeapon = 3
}

public enum MapState
{
	None,
	WaitingToSpawn,
	StartSpawn,
	Spawning,
	SpawnDone,
	WaitForNextSpawn,
}

public enum TypeShipShooter
{
	None = 0,
	Laser = 1,
	Missile = 2,

	All = 100,
}
