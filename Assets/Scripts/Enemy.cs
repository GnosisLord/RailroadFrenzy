﻿using System;
using UnityEngine;

public class Enemy : Vehicle {
	public bool aggroed; //Enemy has detected Player
	public float aggrorange; //range at which the Enemy scans for Player
	public float aggrodelay; //time detween checks for aggro;
	private float aggrotimer; //time remaining until next aggro check;
	public float aimdelay; //time between turning towards Player and firing;
	private float aiming; //time until Enemy starts firing
	public GameObject target;
	public Vector3 desired;

	// Use this for initialization
	public void Start()
	{
		base.Start();
		friendly = false;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
		if (aggroed) {
			Move (Seek ());
			if(aiming>0){
				aiming -= Time.deltaTime;
			}else{
				float arc = Vector3.Angle((target.transform.position - this.transform.position),transform.forward);
				if (arc < 45 || arc >= 315) {
					Fire ();
				}else{
					Face (FindDirection(target));
					aiming = aimdelay;
				}
			}
		} else if (aggrotimer <= 0) {
			aggroed = Aggro ();
			aggrotimer = aggrodelay;
		} else {
			aggrotimer-=Time.deltaTime;
		}




	}
	public bool Aggro(){
		Collider[] nearby = Physics.OverlapSphere (transform.position, aggrorange);
		foreach (Collider obj in nearby) {
			if (obj.gameObject.GetComponent<Player>()!=null){
				return true;
			}
		}
		return false;
		
	}
	public float FindDirection(GameObject target){
		Vector3 adjusted = transform.rotation * (target.transform.position - this.transform.position);
		float angle = Vector3.Angle(adjusted,transform.forward);
		if (angle < 45 ) {
			return 0f;
		}
		if (angle >= 135) {
			return 180f;
		}
		if (Vector3.Angle (adjusted,transform.right)<=90) {
			return 90f;
		}
		return 270f;
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
	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.GetComponent<Vehicle>() != null)
		{
			if (col.gameObject.GetComponent<Vehicle>().friendly != friendly)
			{
				col.gameObject.GetComponent<Vehicle>().Damage(damage);
			}
		}
	}
}
