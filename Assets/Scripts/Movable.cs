using System;
using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour
{
    public bool idle;
    public float movetime;
	public float movementlock;
    public float acceleration;
	public float maxspeed;
    private Vector3 velocity;

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
        else
        {
            idle = true;
        }
		if (movetime > 0) {
			movetime -= Time.deltaTime;
		}
		velocity *= .9f;
    }
    public void Move(Vector3 direction)
    {
		Vector3 local = transform.InverseTransformDirection (direction);
        idle = false;
        movetime = movementlock;
		velocity += local.normalized*acceleration;
    }
	public void Rotate(float angle){
		velocity = Quaternion.AngleAxis (angle, transform.up) * velocity;
	}
	public Vector3 getVelocity(){
		return velocity;
	}
}
