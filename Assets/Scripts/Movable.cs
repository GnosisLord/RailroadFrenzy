using System;
using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour
{
    public bool idle;				//Ready to take additional movement commands
    public float movetime;			//Time until current movement finish and this becomes idle
	public float movementlock;		//If move it called, the direction is locked in for this number of seconds
    public float acceleration;		//Velocity Increase per Move call
	public float maxspeed;			//Velocity magnitude cap
    private Vector3 velocity;		//Current velocity

    public void Start()
    {
        movetime = 0f;
    }
    public void Update()
    {

        if (movementlock == 0 || movetime > 0)
        {
			velocity = Vector3.ClampMagnitude(velocity,maxspeed);
            transform.Translate(velocity * Time.deltaTime);
        }
		if (movetime > 0) {
			movetime -= Time.deltaTime;
		}
		else
		{
			idle = true;
		}
		//Frictional drag
		velocity *= .9f;
    }
	//Moves in direction for Movementlock
    public void Move(Vector3 direction)
    {
		Vector3 local = transform.InverseTransformDirection (direction);
        idle = false;
        movetime = movementlock;
		velocity += local.normalized*acceleration;
    }
	//Rotates the given angle, used to maintain velocity when changing direction
	public void Rotate(float angle){
		velocity = Quaternion.AngleAxis (angle, transform.up) * velocity;
	}
	//getter for velocity
	public Vector3 getVelocity(){
		return velocity;
	}
}
