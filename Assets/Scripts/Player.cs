using System;
using UnityEngine;

public class Player : Vehicle
{
    

    public void Start()
    {
        base.Start();
		friendly = true;
    }
    public void Update()
    {
        base.Update();
        if (Input.GetKey(KeyCode.W))
        {
            Move(new Vector3(0, 0, 1));
        }
        if (Input.GetKey(KeyCode.A))
        {
            Move(new Vector3(-1, 0, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            Move(new Vector3(0, 0, -1));
        }
        if (Input.GetKey(KeyCode.D))
        {
            Move(new Vector3(1, 0, 0));
        }
		if (Input.GetKey(KeyCode.UpArrow))
		{
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				Face (315f);
			}else if(Input.GetKey (KeyCode.RightArrow)){
				Face (45f);
			}else{
				Face(0f);
			}
			Fire ();
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			if (Input.GetKey(KeyCode.UpArrow))
			{
				Face (315f);
			}else if(Input.GetKey (KeyCode.DownArrow)){
				Face (225f);
			}else{
				Face(270f);
			}
			Fire ();
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				Face (225);
			}else if(Input.GetKey (KeyCode.RightArrow)){
				Face (135);
			}else{
				Face(180);
			}
			Fire ();
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			if (Input.GetKey(KeyCode.UpArrow))
			{
				Face (45);
			}else if(Input.GetKey (KeyCode.DownArrow)){
				Face (135);
			}else{
				Face(90f);
			}
			Fire ();
		}
    }
}
