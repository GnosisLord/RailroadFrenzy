using System;
using UnityEngine;

public class Player : Vehicle
{
    private bool boosting;
    private bool attached = false;
    public bool endGame = false;
	public AudioSource healthsfx;
	public AudioSource fuelsfx;
	public AudioSource upgradesfx;
	public AudioSource deathsfx;
    public GameObject end;
    public GameObject helicopter;

    public void Start()
    {
        base.Start();
		friendly = true;
		GameObject.DontDestroyOnLoad (gameObject);
        fuel = fuelmax;
    }
    public override void Update()
    {
        base.Update();

        // Ending level Collision
        if (this.transform.position.x <= end.transform.position.x + 1 &&
            this.transform.position.x >= end.transform.position.x - 1 &&
            this.transform.position.z <= end.transform.position.z + 1 &&
            this.transform.position.z >= end.transform.position.z - 1)
        {
            // End all controls of player object
            endGame = true;


        }

        if (endGame)
        {
            float x, y, z;
            // Lower the copter

            if(!attached)
            {
                if (helicopter.transform.position.y >= 1.8 && !attached)
                {
                    x = helicopter.transform.position.x;
                    y = helicopter.transform.position.y - 1;
                    z = helicopter.transform.position.z;

                    helicopter.transform.position = new Vector3(x, y, z);
                }

                else
                {
                    attached = true;
                }
            }

            else if (attached)
            {
                // Line the player up with the helicopter
                if (helicopter.transform.position.y < 6)
                {
                    x = helicopter.transform.position.x;
                    y = helicopter.transform.position.y - 1;
                    z = helicopter.transform.position.z;

                    this.transform.position = new Vector3(x, y, z);

                    // Raise both objects
                    helicopter.transform.position = new Vector3(x, y + 1.1f, z);
                }

                else
                {
                    x = helicopter.transform.position.x - 0.2f;
                    y = helicopter.transform.position.y;
                    z = helicopter.transform.position.z;

                    helicopter.transform.position = new Vector3(x, y, z);
                    this.transform.position = new Vector3(x, y - 1, z);
                }    
            }         
        }

		//Movement Controls
        if (!endGame)
        {
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
                    Face(315f);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    Face(45f);
                }
                else
                {
                    Face(0f);
                }
                Fire();
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    Face(315f);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    Face(225f);
                }
                else
                {
                    Face(270f);
                }
                Fire();
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    Face(225);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    Face(135);
                }
                else
                {
                    Face(180);
                }
                Fire();
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    Face(45);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    Face(135);
                }
                else
                {
                    Face(90f);
                }
                Fire();
            }
            //Boost Control
            if (Input.GetKey(KeyCode.Space))
            {
                boosting = true;
                invul = .5f;
                movement.setBoost(true);

            }
            else
            {
                boosting = false;
                movement.setBoost(false);
            }
            //Fuel consumption
            if (boosting)
            {
                fuel -= 10 * fueldecay * Time.deltaTime;
            }
            else
            {
                fuel -= fueldecay * Time.deltaTime;
            }
            if (fuel <= 0f)
            {
                Destroy();
            }
            if (invul > 0)
            {
                gameObject.GetComponent<Light>().intensity = 8;
            }
            else
            {
                gameObject.GetComponent<Light>().intensity = 0;
            }
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
	public override void Destroy(){
		deathsfx.Play ();
		base.Destroy ();
	}
}
