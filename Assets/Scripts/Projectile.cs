using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool friendly;	//Friendliness of entity this was fired from, only hits destructibles of opposite friendliness
    public float damage;	//Damage dealt to  destructible on hit
    public float range;		//Time until this destroys itself
    public Vector3 velocity;	//Directional velocity
    public float scale;			//Scale, relative to firing vehicle
	public float blastradius;	//Radius damaged if explosive
	public bool explosive;		//Damages all destructibles in blastradius upon end of range
	public bool homing;			//Seeks nearby destructibles of opposite friendliness
	public float tracking;		//Magnitude of homing force
	
    public void Start()
    {
        transform.localScale = new Vector3(scale, scale, scale);
    }
    public void Update()
    {
		if (homing) {
			Collider[] nearby = Physics.OverlapSphere (transform.position, 10);
			foreach (Collider obj in nearby) {
				if (obj.gameObject.GetComponent<Vehicle>()!=null){
					if (obj.gameObject.GetComponent<Destructible>().friendly != this.friendly){
						velocity = Vector3.RotateTowards(velocity,obj.transform.position-transform.position,tracking*Time.deltaTime,0.0f);
					}
				}
			}
		}
        transform.Translate(velocity * Time.deltaTime);
        range -= Time.deltaTime;
        if (range < 0)
        {
			if(explosive){
				Explode ();
			}
			Destroy(gameObject);
        }
    }
	//Check for collision with destructible or obstacle
	void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.GetComponent<Destructible>() != null)
        {
            if (col.gameObject.GetComponent<Destructible>().friendly != this.friendly)
            {
				col.gameObject.GetComponent<Destructible>().Damage(damage);
				Destroy(this.gameObject);
            }
            
        }if (col.gameObject.CompareTag ("Obstacle")) {
			Destroy(this.gameObject);
		}
    }
	//Damages opposite friendliness destructibles within blastradius
	void Explode(){
		Collider[] nearby = Physics.OverlapSphere (transform.position, blastradius);
		foreach (Collider obj in nearby) {
			if (obj.gameObject.GetComponent<Destructible> () != null) {
				if (obj.gameObject.GetComponent<Destructible>().friendly != this.friendly)
				{
					obj.gameObject.GetComponent<Destructible>().Damage(damage);

				}
			}
		}
		Destroy(this.gameObject);
	}
	//Setting nonstandard projectile type tag
	public void Special(bool homing, bool explosive){
		this.homing = homing;
		this.explosive = explosive;
	}
	//Setting projectible statistics
	public void Stats(Vector3 velocity, float damage, float range, float scale){
		this.velocity = velocity;
		this.damage = damage;
		this.range = range;
		this.scale = scale;
		transform.localScale = new Vector3(scale, scale, scale);
	}
}
