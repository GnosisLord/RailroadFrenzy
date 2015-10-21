using System;
using UnityEngine;

public class Player : Vehicle
{
	public float fueldecay = .5f;		//Amount of Fuel consumed per second
	public float fuelmax = 100f;		//Maximum Fuel level
    public float fuel;        //Current Fuel, acts as a timer
    private bool boosting;
	public AudioSource healthsfx;
	public AudioSource fuelsfx;
	public AudioSource upgradesfx;
	public AudioSource deathsfx;

    public void Start()
    {
        base.Start();
		friendly = true;
		GameObject.DontDestroyOnLoad (gameObject);
        fuel = fuelmax;
    }
    public void Update()
    {
        base.Update();
		//Movement Controls
        if (Input.GetKey(KeyCode.W))
        {
            Move(new Vector3(0, 0, 1));
        }
        if (Input.GetKey(KeyCode.A))
        {
            Move(new Vector3(-1, 0, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            Move(new Vector3(0, 0, -1));
        }
        if (Input.GetKey(KeyCode.D))
        {
            Move(new Vector3(1, 0, 0));
        }
		//Firing Controls
		if (Input.GetKey(KeyCode.UpArrow))
		{
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				Face (315f);
			}else if(Input.GetKey (KeyCode.RightArrow)){
				Face (45f);
			}else{
				Face(0f);
			}
			Fire ();
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			if (Input.GetKey(KeyCode.UpArrow))
			{
				Face (315f);
			}else if(Input.GetKey (KeyCode.DownArrow)){
				Face (225f);
			}else{
				Face(270f);
			}
			Fire ();
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				Face (225);
			}else if(Input.GetKey (KeyCode.RightArrow)){
				Face (135);
			}else{
				Face(180);
			}
			Fire ();
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			if (Input.GetKey(KeyCode.UpArrow))
			{
				Face (45);
			}else if(Input.GetKey (KeyCode.DownArrow)){
				Face (135);
			}else{
				Face(90f);
			}
			Fire ();
		}
		//Boost Control
		if(Input.GetKey (KeyCode.Space)){
			boosting = true;
			invul = .5f;
			movement.setBoost(true);

		}else{
			boosting = false;
			movement.setBoost(false);
		}
		//Fuel consumption
		if (boosting) {
			fuel -= 10*fueldecay;
		} else {
			fuel -= fueldecay;
		}
		if (fuel <= 0f) {
			Destroy ();
		}
		if (invul > 0) {
			gameObject.GetComponent<Light> ().intensity = 8;
		} else {
			gameObject.GetComponent<Light> ().intensity = 0;
		}
    }
	public void Upgrade(float damage, float hp, float maxFuel,float firerate, float shotscale, float range, float shotspeed, float speed, float scale){
		base.Upgrade (damage, hp, maxFuel, firerate, shotscale, range, shotspeed, speed, scale);
		upgradesfx.Play ();
	}
	//Restores fuel by amount
	public void refuel(float amount){
		fuel += amount;
		fuelsfx.Play ();
	}
	public void Heal(float healing){
		base.Heal (healing);
		healthsfx.Play ();
	}
	public void Destroy(){
		deathsfx.Play ();
		base.Destroy ();
	}
}
