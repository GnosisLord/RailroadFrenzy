using System;
using UnityEngine;

public class Vehicle : Destructible
{
    protected float damage;
    protected float firerate;
    protected float firecd;
    protected float shotscale;
    protected float range;
    protected float shotspeed;

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
    public void fire(Vector3 direction){
        if (firecd <= 0)
        {
            transform.Rotate(transform.forward - direction);
            //GameObject instantiatedProjectile = Instantiate (projectile, transform.position, direction);
            firecd = 1 / firerate;
        }
    }
}
