using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private bool playerMuerto;

    private void Awake()
    {
        muerto = false;
        playerMuerto = false;
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
        else if (player.GetComponent<CharController>().life <= 0 && !playerMuerto)
        {
            playerMuerto = true;
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

            if (distancia > nav.stoppingDistance)//Distancia a la que se vuelve a activar el agente
            {
                anim.SetBool("ataque", false);
                AtacBool = false;
                CancelInvoke("Ataque");
                nav.speed = speed;
                nav.destination = target.position;   
            }

            if(distancia <= nav.stoppingDistance)//Distancia a la que se desactiva el agente
            {
                nav.speed = 0;
                nav.destination = transform.position;
            }

            if (distancia <= nav.stoppingDistance && AtacBool == false && player.GetComponent<CharacterController>().isGrounded)
            {
                
                anim.SetBool("ataque", true);
                AtacBool = true;
                Invoke("Ataque", 1.1f);//tiempo que tarda en dar el golpe
                
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
                GetComponent<AudioSource>().Stop();
            }
        }
        
    }

    void Ataque()
    {
        Invoke("Ataque", 2.6f);
        if (distancia <= 1.2 && AtacBool && !muerto && !playerMuerto && player.GetComponent<CharacterController>().isGrounded)
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
        if (PlayerPrefs.GetString("WinEnemy2") == "Yes") PlayerPrefs.SetString("EnemyVictory", "Yes");
        if (PlayerPrefs.GetString("WinEnemy1") == "No") PlayerPrefs.SetString("WinEnemy1", "Yes");
        else PlayerPrefs.SetString("WinEnemy2", "Yes");
        yield return new WaitForSeconds(5);
        if(PlayerPrefs.GetString("EnemyVictory")!="Yes") SceneManager.LoadScene("GameScene");
    }
}
