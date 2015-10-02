using System;
using UnityEngine;

public class Player : Vehicle
{
    public Movable movement;

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
            movement.Move(new Vector3(0, 0, 1));
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement.Move(new Vector3(-1, 0, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.Move(new Vector3(0, 0, -1));
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.Move(new Vector3(1, 0, 0));
        }
		if (Input.GetKey(KeyCode.UpArrow))
		{
			Fire(new Vector3(0, 0, 1));
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			Fire(new Vector3(-1, 0, 0));
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			Fire(new Vector3(0, 0, -1));
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			Fire(new Vector3(1, 0, 0));
		}
    }
}
