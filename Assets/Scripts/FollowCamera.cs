using System;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject target;
	public void Start(){
        
    }

    public void Update(){
        transform.position = new Vector3(target.transform.position.x,25 + 5 * target.transform.localScale.y,target.transform.position.z);
    }
}
