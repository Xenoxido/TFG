using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoController : MonoBehaviour
{
    public float Vida = 100;//vida enemigo
    public Animator anim;
    public NavMeshAgent nav;
    public GameObject player;
    public Transform target;//poner objeto al que sigue el enemigo
    public int ataque;//vida que saca el enemigo al player al atacar
    public bool AtacBool;
    public CharacterController character;
    public float distancia;//distancia que hay entre el player y el enemigo
    public int muerte;
    public float speed;
    public bool muerto;
    public float MaxVida;

    private void Awake()
    {
        muerto = false;
        speed = nav.speed;
        MaxVida = Vida;
    }
    // Start is called before the first frame update
    void Start()
    {
        muerte = Random.Range(1, 3);
        character.enabled = true;
        AtacBool = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }
        else if (player.GetComponent<CharController>().life <= 0)
        {
            StartCoroutine(PlayerDied());
            return;
        }
        else if (!muerto)
        {

            //CALCULAR DISTANCIA Y DIRECCION
            distancia = Vector3.Distance(transform.position, target.position);
            Vector3 movement = Vector3.zero;
            Vector3 heading = target.position - transform.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;
            movement.x = Mathf.Sign(direction.x);
            transform.rotation = Quaternion.LookRotation(movement);

            if (distancia >= 2)//Distancia a la que se vuelve a activar el agente
            {
                anim.SetBool("ataque", false);
                nav.speed = speed;
                nav.destination = target.position;   
            }

            if(distancia <= nav.stoppingDistance)//Distancia a la que se desactiva el agente
            {
                nav.speed = 0;
                nav.destination = transform.position;
            }

            if (distancia < nav.stoppingDistance && AtacBool == false && player.GetComponent<CharacterController>().isGrounded)
            {
                
                anim.SetBool("ataque", true);

                Invoke("Ataque", 1.1f);//tiempo que tarda en dar el golpe
                AtacBool = true;
            }

            //VIDA
            if (Vida <= 0)
            {
                muerto = true;
                nav.speed = 0;
                character.enabled = false;

                if (muerte == 1)
                {
                    anim.SetBool("muerte1", true);
                }

                if (muerte == 2)
                {
                    anim.SetBool("muerte2", true);
                }
            }
        }
        
    }

    void Ataque()
    {
        Invoke("AF", 1.5f);
        if (distancia <= 2 && AtacBool == true && !muerto)
        {
            player.SendMessage("HurtLife", ataque);
        }
    }

    void AF()
    {
        AtacBool = false;
    }

    void HurtLife(int damage)
    {
        Vida -= damage;
    }

    IEnumerator PlayerDied()
    {
        anim.SetBool("AIdle", true);
        yield return new WaitForSeconds(1.2f);
        transform.position = target.position;
        anim.SetBool("playerMuerto", true);
    }
}
