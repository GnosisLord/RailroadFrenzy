using System;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float hp;
    public float hpmax;
    public float invul;
    public float invulduration;
    public float scale;
    public bool friendly;

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
    public void Damage(float damage)
    {
        if (invul <= 0) {
			hp -= damage;
			if (hp <= 0) {
				Destroy ();
			} else {
				invul = invulduration;
			}
		} 
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
	public void Heal(float healing){
		hp += healing;
		if (hp > hpmax) {
			hp = hpmax;
		}
	}
}
