using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool friendly;
    public float damage;
    public float range;
    public Vector3 velocity;
    public float scale;

	public Projectile()
	{

	}
    public void Start()
    {
        transform.localScale = new Vector3(scale, scale, scale);
    }
    public void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
        range -= Time.deltaTime;
        if (range < 0)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<Destructible>() != null)
        {
            if (col.gameObject.GetComponent<Destructible>().friendly != friendly)
            {
                col.gameObject.GetComponent<Destructible>().Damage(damage);
            }
            Destroy(gameObject);
        }
    }
	public void Stats(Vector3 velocity, float damage, float range, float scale){
		this.velocity = velocity;
		this.damage = damage;
		this.range = range;
		this.scale = scale;
		transform.localScale = new Vector3(scale, scale, scale);
	}
}
