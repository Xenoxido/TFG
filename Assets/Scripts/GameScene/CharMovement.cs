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
    //Booleanos para los golpes
    private bool hoop = false, upper = false, lowkick = false, kick = false, jump = false;
    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _vertSpeed = minFall;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;
        float horInput = Input.GetAxis("Horizontal");
        //Golpes: Puñetazo, Gancho, Patada baja, Patada alta
        if (Input.GetKeyDown(KeyCode.J) && !jump && !kick && !lowkick && !upper) //Reconocimiento de la patada, la corutina lo para
        {
            _animator.SetBool("Hoop", true);
            hoop = true;
            StartCoroutine(offHoop());
        }

        if (Input.GetKeyDown(KeyCode.K) && !jump && !kick && !lowkick && !hoop) //Reconocimiento de la patada, la corutina lo para
        {
            _animator.SetBool("Upper", true);
            upper = true;
            StartCoroutine(offUpper());
        }

        if (Input.GetKeyDown(KeyCode.U) && !jump && !hoop && !kick && !upper) //Reconocimiento de la patada, la corutina lo para
        {
            _animator.SetBool("LowKick", true);
            lowkick = true;
            StartCoroutine(offLowKick());
        }

        if (Input.GetKeyDown(KeyCode.I) && !jump && !hoop && !lowkick && !upper) //Reconocimiento de la patada, la corutina lo para
        {
            _animator.SetBool("Kick", true);
            kick = true;
            StartCoroutine(offKick());
        }
        if (kick || hoop || lowkick || upper) horInput = 0.0f;

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
            jump = false;
            if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)) && !hoop && !upper && !lowkick && !kick)
            {
                _vertSpeed = jumpSpeed;
                jump = true;
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
    //Corrutinas para detener las animaciones de los golpes
    private IEnumerator offHoop()
    {
        yield return new WaitForSeconds(1);
        _animator.SetBool("Hoop", false);
        yield return new WaitForSeconds(.5f);
        hoop = false;
    }

    private IEnumerator offUpper()
    {
        yield return new WaitForSeconds(1);
        _animator.SetBool("Upper", false);
        yield return new WaitForSeconds(.5f);
        upper = false;
    }

    private IEnumerator offLowKick()
    {
        yield return new WaitForSeconds(1);
        _animator.SetBool("LowKick", false);
        yield return new WaitForSeconds(.5f);
        lowkick = false;
    }
    
    private IEnumerator offKick()
    {
        yield return new WaitForSeconds(1);
        _animator.SetBool("Kick", false);
        yield return new WaitForSeconds(.5f);
        kick = false;
    }
    
}
