using System;
using UnityEngine;

public class Vehicle : Destructible
{
    public float damage;
    public float firerate;
    protected float firecd;
    public float shotscale;
    public float range;
    public float shotspeed;
	public GameObject shot;
	public Vector3 shotoffset;
	public Movable movement;

	public bool explosiveshot;
	public bool homingshot;
	public bool backshot;
	public bool quadshot;
	
    public void Start()
    {
        base.Start();
        firecd = 0f;

    }
    public void Update()
    {
        base.Update();
        if(firecd>0){
            firecd -= Time.deltaTime;
        }
    }
	public void Move(Vector3 direction){
		movement.Move (direction);
	}
	public void Face(float angle){
		float r = angle - transform.rotation.eulerAngles.y;
		transform.Rotate(new Vector3(0f,r,0f));
		movement.Rotate (-r);
	}
    public void Fire(){
        if (firecd <= 0)
        {
			shot.GetComponent<Projectile>().Stats(transform.forward*shotspeed,damage,range,shotscale);
			shot.GetComponent<Projectile>().Special (homingshot,explosiveshot);
           	Instantiate (shot, transform.position+(transform.rotation*shotoffset), new Quaternion(0f,0f,0f,0f));
			if (quadshot) {
				Instantiate (shot, transform.position+(transform.rotation*shotoffset), new Quaternion(0f, 0.7071f,0f, 0.7071f));
				Instantiate (shot, transform.position+(transform.rotation*shotoffset), new Quaternion(0f,1f,0f,0f));
				Instantiate (shot, transform.position+(transform.rotation*shotoffset), new Quaternion(0f, -0.7071f,0f, 0.7071f));
			}else if(backshot){
				Instantiate (shot, transform.position+(transform.rotation*shotoffset), new Quaternion(0f,1f,0f,0f));
			}
            firecd = 1 / firerate;
        }

    }
	public void Upgrade(float damage, float hp, float firerate, float shotscale, float range, float shotspeed, float speed, float scale){
		this.damage += damage;
		this.hpmax += hp;
		this.hp += hp;
		this.firerate += firerate;
		this.shotscale += shotscale;
		this.range += range;
		this.shotspeed += shotspeed;
		movement.maxspeed += speed;
		this.scale += scale;
	}
	public void Destroy()
	{
		GameController.get ().Death (this);
		Destroy(gameObject);
	}
}
