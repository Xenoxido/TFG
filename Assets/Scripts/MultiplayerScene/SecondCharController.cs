﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class SecondCharController : MonoBehaviour
{
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip punch;
    //Player Parameters
    public float life;
    public float MaxVida;
    public bool muerto;
    private bool reached;
    private bool enemigoMuerto;
    
    //Physics Parameters
    public float moveSpeed = 6.0f;
    private CharacterController _charController;
    private Animator _animator;
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;
    private float _vertSpeed;
    private float dirX;

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

    private Coroutine co;


    //Ya no se usan estos bools
    //bool hoop = false, upper = false, lowkick = false, kick = false,

    private void Awake()
    {
        muerto = false;
        reached = false;
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
        if (enemy == null)
        {
            enemy = GameObject.FindGameObjectWithTag("Player");

        }else if (!muerto && !enemigoMuerto && !reached)
        {
            if (life <= 0)
            {
                muerto = true;
                _animator.SetBool("Dead", true);
                StartCoroutine(Died());
            }

            if(enemy.GetComponent<FirstCharController>().life <= 0 && !golpe)
            {
                enemigoMuerto = true;
                _animator.SetBool("Win", true);
                StartCoroutine(EnemyDied());
            }

            Vector3 movement = Vector3.zero;
            float horInput = 0.0f;
            if (Input.GetKey(KeyCode.LeftArrow)) horInput = -1.0f;
            else if (Input.GetKey(KeyCode.RightArrow)) horInput = 1.0f;

            //Calculo de distancias y direcciones
            distancia = Vector3.Distance(transform.position, enemy.transform.position);

            Vector3 heading = enemy.transform.position - transform.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;
            dirX = direction.x;
            //Debug.Log("Direction: "+direction.x+" rotation: "+transform.rotation.y);

            //Golpes: Puñetazo, Gancho, Patada baja, Patada alta
            if (Input.GetKeyDown(KeyCode.Keypad5) && !jump && !golpe) //Reconocimiento de la patada, la corutina lo para
            {
                _animator.SetBool("Hoop", true);
                golpe = true;
                co = StartCoroutine(J());
            }

            if (Input.GetKeyDown(KeyCode.Keypad6) && !jump && !golpe && !reached) //Reconocimiento de la patada, la corutina lo para
            {
                _animator.SetBool("Upper", true);
                golpe = true;
                //Debug.Log("Start");
                co = StartCoroutine(K());
            }

            if (Input.GetKeyDown(KeyCode.Keypad8) && !jump && !golpe) //Reconocimiento de la patada, la corutina lo para
            {
                _animator.SetBool("LowKick", true);
                golpe = true;
                co = StartCoroutine(U());
            }

            if (Input.GetKeyDown(KeyCode.Keypad9) && !jump && !golpe) //Reconocimiento de la patada, la corutina lo para
            {
                _animator.SetBool("Kick", true);
                golpe = true;
                co = StartCoroutine(I());
            }

            if (golpe) horInput = 0.0f;


            //Debug.Log(!(Mathf.Sign(horInput) == Mathf.Sign(direction.x) && distancia <= 1f));

            
            if (horInput != 0 && ((!(Mathf.Sign(horInput) == Mathf.Sign(direction.x)  && distancia <= 1f)) || enemy.GetComponent<FirstCharController>().muerto))
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
                if (Input.GetKeyDown(KeyCode.UpArrow) && !golpe)
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
        yield return new WaitForSeconds(Jtime);
        if (distancia <= 1.2 && !enemigoMuerto && !muerto && !reached && enemy != null && Mathf.Sign(transform.rotation.y) == Mathf.Sign(dirX)) { enemy.SendMessage("HurtLife", JDamage); audio.PlayOneShot(punch); }
        _animator.SetBool("Hoop", false);
        yield return new WaitForSeconds(.05f);
        golpe = false;
    }

    private IEnumerator K()
    {
        yield return new WaitForSeconds(Ktime);
        if (distancia <= 1.2 && !enemigoMuerto && !muerto && !reached && enemy != null && Mathf.Sign(transform.rotation.y) == Mathf.Sign(dirX)) { enemy.SendMessage("HurtLife", KDamage); audio.PlayOneShot(punch); }
        _animator.SetBool("Upper", false);
        yield return new WaitForSeconds(.05f);
        golpe = false;
    }

    private IEnumerator U()
    {
        yield return new WaitForSeconds(Utime);
        if (distancia <= 1.2 && !enemigoMuerto && !muerto && !reached && enemy != null && Mathf.Sign(transform.rotation.y) == Mathf.Sign(dirX)) { enemy.SendMessage("HurtLife", UDamage); audio.PlayOneShot(punch); }
        _animator.SetBool("LowKick", false);
        yield return new WaitForSeconds(.05f);
        golpe = false;
    }
    
    private IEnumerator I()
    {
        
        yield return new WaitForSeconds(Itime);
        if (distancia <= 1.2 && !enemigoMuerto && !muerto && !reached && enemy != null && Mathf.Sign(transform.rotation.y) == Mathf.Sign(dirX)) {  enemy.SendMessage("HurtLife", IDamage); audio.PlayOneShot(punch); }
        _animator.SetBool("Kick", false);
        yield return new WaitForSeconds(.05f);
        golpe = false;
    }

    private IEnumerator Died()
    {
        yield return new WaitForSeconds(0.0000001f);
        _animator.SetBool("Dead", false);
    }

    public void HurtLife(int damage)
    {
        if (co != null) StopCoroutine(co);
        //animacion golpe
        reached = true;
        _animator.SetBool("Reached", true);
        StartCoroutine(ReachReturn());
        //quitar vida
        life -= damage;
        //Debug.Log("Hurted: " + damage.ToString() + ", Total Life: " + life.ToString());
    }

    private IEnumerator ReachReturn()
    {
        yield return new WaitForSeconds(0.0000001f);
        _animator.SetBool("Reached", false);
        _animator.SetBool("Hoop", false);
        _animator.SetBool("Upper", false);
        _animator.SetBool("LowKick", false);
        _animator.SetBool("Kick", false);
        yield return new WaitForSeconds(0.5f);
        reached = false;
        golpe = false;

    }

    private IEnumerator EnemyDied()
    {
        yield return new WaitForSeconds(0.0000001f);
        _animator.SetBool("Win", false);
        if (PlayerPrefs.GetString("WinEnemy2") == "Yes") PlayerPrefs.SetString("EnemyVictory", "Yes");
        if (PlayerPrefs.GetString("WinEnemy1") == "No") PlayerPrefs.SetString("WinEnemy1", "Yes");
        else PlayerPrefs.SetString("WinEnemy2", "Yes");
        yield return new WaitForSeconds(5);
        if(PlayerPrefs.GetString("EnemyVictory")!="Yes")SceneManager.LoadScene("MultiplayerScene");
    }

}
