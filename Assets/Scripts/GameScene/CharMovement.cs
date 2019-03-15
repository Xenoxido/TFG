using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharMovement : MonoBehaviour
{
    public float moveSpeed = 6.0f;
    private CharacterController _charController;
    private Animator _animator;
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;
    private float _vertSpeed;
    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();        _vertSpeed = minFall;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;
        float horInput = Input.GetAxis("Horizontal");

        if (horInput != 0)
        {
            movement.x = horInput * moveSpeed;
            //Quaternion rot = Quaternion.Euler(0, 360, 0);
            //movement = rot * movement;
            transform.rotation = Quaternion.LookRotation(movement);
        }

        _animator.SetFloat("Speed", movement.sqrMagnitude);
        if (_charController.isGrounded)
        {
            if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W))
            {
                _vertSpeed = jumpSpeed;
            }
            else
            {
                _vertSpeed = minFall;
                _animator.SetBool("Jumping", false);
            }
        }
        else
        {
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVelocity)
            {
                _vertSpeed = terminalVelocity;
            }
            _animator.SetBool("Jumping", true);
        }
        movement.y = _vertSpeed;

        movement *= Time.deltaTime;
        _charController.Move(movement);
    }
}
