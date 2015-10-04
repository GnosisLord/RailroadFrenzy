using System;
using UnityEngine;

public class Enemy : Vehicle {

	public Vehicle target;
	public Vector3 desired;

	// Use this for initialization
	public void Start()
	{
		base.Start();
		friendly = true;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
		Move (Seek ());

		// Fire at the player

	}

	// Seek the target
	Vector3 Seek(){
		// vector between the target and the enemy
		desired = target.transform.position - this.transform.position;

		// nullify y translation
		desired.y = 0;

		// determine smaller axis to travel
		if (Math.Abs (desired.x) > Math.Abs (desired.z)) {
			desired.z = 0;
			return desired;
		} 
		else {
			desired.x = 0;
			return desired;
		}
	}
}
