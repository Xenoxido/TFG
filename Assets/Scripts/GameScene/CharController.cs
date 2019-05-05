﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharController : MonoBehaviour
{
    //Player Parameters
    public float life;
    public float MaxVida;
    private bool muerto;
    private bool enemigoMuerto;
    
    //Physics Parameters
    private float moveSpeed = 6.0f;
    private CharacterController _charController;
    private Animator _animator;
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;
    private float _vertSpeed;

    //Booleanos para los golpes
    private bool golpe = false, jump = false;
    private GameObject enemy;
    private float distancia;
    public float Jtime;
    public float Ktime;
    public float Utime;
    public float Itime;
    public float JDamage;
    public float KDamage;
    public float UDamage;
    public float IDamage;


    //Ya no se usan estos bools
    //bool hoop = false, upper = false, lowkick = false, kick = false,

    private void Awake()
    {
        muerto = false;
        enemigoMuerto = false;
        MaxVida = life;
    }
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
        if(enemy == null)
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy");
           
        }else if (!muerto && !enemigoMuerto)
        {
            if (life <= 0)
            {
                muerto = true;
                _animator.SetBool("Dead", true);
                StartCoroutine(Died());
            }

            if(enemy.GetComponent<EnemigoController>().Vida <= 0 && !golpe)
            {
                enemigoMuerto = true;
                _animator.SetBool("Win", true);
                StartCoroutine(EnemyDied());
            }

            Vector3 movement = Vector3.zero;
            float horInput = Input.GetAxis("Horizontal");

            //Calculo de distancias y direcciones
            distancia = Vector3.Distance(transform.position, enemy.transform.position);

            Vector3 heading = enemy.transform.position - transform.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;
            //Debug.Log(direction.ToString());

            //Golpes: Puñetazo, Gancho, Patada baja, Patada alta
            if (Input.GetKeyDown(KeyCode.J) && !jump && !golpe) //Reconocimiento de la patada, la corutina lo para
            {
                _animator.SetBool("Hoop", true);
                golpe = true;
                StartCoroutine(J());
            }

            if (Input.GetKeyDown(KeyCode.K) && !jump && !golpe) //Reconocimiento de la patada, la corutina lo para
            {
                _animator.SetBool("Upper", true);
                golpe = true;
                StartCoroutine(K());
            }

            if (Input.GetKeyDown(KeyCode.U) && !jump && !golpe) //Reconocimiento de la patada, la corutina lo para
            {
                _animator.SetBool("LowKick", true);
                golpe = true;
                StartCoroutine(U());
            }

            if (Input.GetKeyDown(KeyCode.I) && !jump && !golpe) //Reconocimiento de la patada, la corutina lo para
            {
                _animator.SetBool("Kick", true);
                golpe = true;
                StartCoroutine(I());
            }

            if (golpe) horInput = 0.0f;


            //Debug.Log(!(Mathf.Sign(horInput) == Mathf.Sign(direction.x) && distancia <= 1f));

            
            if (horInput != 0 && ((!(Mathf.Sign(horInput) == Mathf.Sign(direction.x)  && distancia <= 1f)) || enemy.GetComponent<EnemigoController>().muerto))
            {
                //Debug.Log(horInput);
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
        
    }
    //Corrutinas para detener las animaciones de los golpes
    private IEnumerator J()
    {
        if(distancia < 2 && enemy != null) enemy.SendMessage("HurtLife", JDamage);
        yield return new WaitForSeconds(Jtime);
        _animator.SetBool("Hoop", false);
        yield return new WaitForSeconds(.05f);
        golpe = false;
    }

    private IEnumerator K()
    {
        
        yield return new WaitForSeconds(Ktime);
        if (distancia < 2 && enemy != null) enemy.SendMessage("HurtLife", KDamage);
        _animator.SetBool("Upper", false);
        yield return new WaitForSeconds(.05f);
        golpe = false;
    }

    private IEnumerator U()
    {
        
        yield return new WaitForSeconds(Utime);
        if (distancia < 2 && enemy != null) enemy.SendMessage("HurtLife", UDamage);
        _animator.SetBool("LowKick", false);
        yield return new WaitForSeconds(.05f);
        golpe = false;
    }
    
    private IEnumerator I()
    {
        
        yield return new WaitForSeconds(Itime);
        if (distancia < 2 && enemy != null) enemy.SendMessage("HurtLife", IDamage);
        _animator.SetBool("Kick", false);
        yield return new WaitForSeconds(.05f);
        golpe = false;
    }

    private IEnumerator Died()
    {
        yield return new WaitForSeconds(0.01f);
        _animator.SetBool("Dead", false);
    }

    private IEnumerator EnemyDied()
    {
        yield return new WaitForSeconds(0.5f);
        _animator.SetBool("Win", false);
    }



    public void HurtLife(int damage)
    {
        life -= damage;
        Debug.Log("Hurted: " + damage.ToString() + ", Total Life: " + life.ToString());
    }
    
}