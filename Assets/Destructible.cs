using System;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    protected float hp;
    protected float hpmax;
    protected float invul;
    protected float invulduration;
    protected float scale;
    public bool friendly;

    public void Start()
    {
        hp = hpmax;
        invul = 0f;
    }
    public void Update()
    {
    }
    public void Damage(float damage)
    {
        if(invul<=0){
            hp -= damage;
            if(hp<=0){
                Destroy();
            }
            else{
                invul = invulduration;
            }
        }

    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
