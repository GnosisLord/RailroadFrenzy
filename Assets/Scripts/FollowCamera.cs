using System;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject target;	//Follow target
    public bool endGame = false;
	public void Start(){
        
    }

    public void Update(){

        if (target.gameObject.GetComponent<Player>() != null)
        {
            endGame = target.gameObject.GetComponent<Player>().endGame;

            if (!endGame)
            {
                transform.position = new Vector3(target.transform.position.x, 10 + 50 * target.transform.localScale.y, target.transform.position.z);
            }
        }
    }
}
