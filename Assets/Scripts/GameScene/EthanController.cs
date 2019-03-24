using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EthanController : MonoBehaviour
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
    private bool golpe = false, jump = false;
    //Ya no se usan estos bools
    //bool hoop = false, upper = false, lowkick = false, kick = false,
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
        if (Input.GetKeyDown(KeyCode.J) && !jump && !golpe) //Reconocimiento de la patada, la corutina lo para
        {
            _animator.SetBool("Hoop", true);
            golpe = true;
            StartCoroutine(offHoop());
        }

        if (Input.GetKeyDown(KeyCode.K) && !jump && !golpe) //Reconocimiento de la patada, la corutina lo para
        {
            _animator.SetBool("Upper", true);
            golpe = true;
            StartCoroutine(offUpper());
        }

        if (Input.GetKeyDown(KeyCode.U) && !jump && !golpe) //Reconocimiento de la patada, la corutina lo para
        {
            _animator.SetBool("LowKick", true);
            golpe = true;
            StartCoroutine(offLowKick());
        }

        if (Input.GetKeyDown(KeyCode.I) && !jump && !golpe) //Reconocimiento de la patada, la corutina lo para
        {
            _animator.SetBool("Kick", true);
            golpe = true;
            StartCoroutine(offKick());
        }
        if (golpe) horInput = 0.0f;

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
            if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)) && !golpe)
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
        yield return new WaitForSeconds(.5f);
        _animator.SetBool("Hoop", false);
        yield return new WaitForSeconds(.05f);
        golpe = false;
    }

    private IEnumerator offUpper()
    {
        yield return new WaitForSeconds(.5f);
        _animator.SetBool("Upper", false);
        yield return new WaitForSeconds(.05f);
        golpe = false;
    }

    private IEnumerator offLowKick()
    {
        yield return new WaitForSeconds(.65f);
        _animator.SetBool("LowKick", false);
        yield return new WaitForSeconds(.05f);
        golpe = false;
    }
    
    private IEnumerator offKick()
    {
        yield return new WaitForSeconds(.65f);
        _animator.SetBool("Kick", false);
        yield return new WaitForSeconds(.05f);
        golpe = false;
    }
    
}
