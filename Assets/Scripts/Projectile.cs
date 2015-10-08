using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool friendly;
    public float damage;
    public float range;
    public Vector3 velocity;
    public float scale;
	public float blastradius;
	public bool explosive;
	public bool homing;
	public float tracking;
	
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
	void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.GetComponent<Destructible>() != null)
        {
            if (col.gameObject.GetComponent<Destructible>().friendly != this.friendly)
            {
				col.gameObject.GetComponent<Destructible>().Damage(damage);
				Destroy(this.gameObject);
            }
            
        }
    }
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
	public void Special(bool homing, bool explosive){
		this.homing = homing;
		this.explosive = explosive;
	}
	public void Stats(Vector3 velocity, float damage, float range, float scale){
		this.velocity = velocity;
		this.damage = damage;
		this.range = range;
		this.scale = scale;
		transform.localScale = new Vector3(scale, scale, scale);
	}
}
