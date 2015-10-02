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

	public Vehicle() : base()
	{
        
	}
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
    public void Fire(Vector3 direction){
        if (firecd <= 0)
        {
			Vector3 local = transform.InverseTransformVector(direction);
			transform.LookAt(transform.position+local);
			shot.GetComponent<Projectile>().Stats(direction*shotspeed,damage,range,shotscale);
           	GameObject instantiatedProjectile = (GameObject)Instantiate (shot, transform.position+shotoffset, transform.rotation);
            firecd = 1 / firerate;
        }
    }
}
