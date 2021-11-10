using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float Speed;
    public CharacterController CharController;

    private float walkSpeed;
    private float runSpeed;
    private void Start()
    {
        walkSpeed = Speed;
        runSpeed = Speed / 8;
    }
    private void Update()
    {
        PlayerMoves();
    }

    private void PlayerMoves()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //left shift button to run
        if (Input.GetButton("Fire3"))
        {
            Speed = walkSpeed * runSpeed;
        }
        else
        {
            Speed = walkSpeed;
        }

        Vector3 move = transform.right * x + transform.forward * z;
        this.CharController.Move(move * this.Speed * Time.deltaTime);
    }
}
