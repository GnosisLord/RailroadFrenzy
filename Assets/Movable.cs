using System;
using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour
{
    public bool idle;
    public float movetime;
    public float speed;
    private Vector3 velocity;
    protected CharacterController characterController;

    public void Start()
    {
        movetime = 0f;
        characterController = gameObject.GetComponent<CharacterController>();
    }
    public void Update()
    {
        if (movetime > 0)
        {
            movetime -= Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }
        else
        {
            idle = true;
        }
    }
    public void Move(Vector3 direction)
    {
        idle = false;
        movetime = 0.25f;
        velocity = direction;
    }
}
