using System;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float hp;			//Current Health (Set to max on start)
    public float hpmax=5f;			//Initial/Max Health
    public float invul; 		//Time until can be damaged again
    public float invulduration=.1f;	//Delay after being damaged until this can be damaged again
    public float scale=1f;			//Scale (all axes)
    public bool friendly=false;		//Allied with player. Entities can only damage destructibles with opposite friendly value
	public bool immortal=false;		//Does not take damage;

    public void Start()
    {
        hp = hpmax;
        invul = 0f;

    }
    public void Update()
    {
		transform.localScale = new Vector3(scale,scale,scale);
		if (invul > 0) {
			invul -= Time.deltaTime;
		}
    }

	//Reduce HP by damage if not invulnerable and initiate invulnerability
    public void Damage(float damage)
    {
        if (invul <= 0 && !immortal) {
			hp -= damage;
			if (hp <= 0) {
				Destroy ();
			} else {
				invul = invulduration;
			}
		} 
    }
	//This is destroyed, called if HP = 0
    public void Destroy()
    {
        Destroy(gameObject);
    }
	//Restore HP by healing up to hpmax
	public void Heal(float healing){
		hp += healing;
		if (hp > hpmax) {
			hp = hpmax;
		}
	}
}
