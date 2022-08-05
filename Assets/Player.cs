using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public CharacterController controller;

    //CONTROL PANEL

    //movement
    public float speed = 12f;
    public float sprintspeed = 18f;
    public float gravity = -18f; //-9.81
    public float jumpHeight = 2f;

    //health
    public float health;
    public float currentHealth;
    public float maxHealth = 100f;
    //public HealthBar healthbar;
    public float damage = 0.08f;

    //touching the ground
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Lava") //om den tr�ffar objekt med tagen lava
        {
            TakeDamage(0.08f);  //funktion takedamage
            Debug.Log("touched the lava");  //skriv i logs att det nuddade
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;     //n�r spelet b�rjar s�tts hp till fullt
        //healthbar.SetMaxHealth(maxHealth);
    }

    void TakeDamage(float damage) //funktion som heter takedamage
    {
        currentHealth -= damage;
        //healthbar.SetHealth(currentHealth);
    }

    void die() //funktion som �r att d�
    {
        Cursor.lockState = CursorLockMode.Confined; //l�ser upp muspekaren igen
        SceneManager.LoadScene(2); //ladda game over scene
    }
    // Update is called once per frame
    void Update()
    {

        if (currentHealth <= 0) //om hp �r mindre �n ELLER LIKA MED 0
        {
            die(); //k�r funktionen die
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //checkar om den �r grounded eller inte (beroende p� groundCheck)

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //MOVING

        if (Input.GetKeyDown(KeyCode.LeftShift)) //om man h�ller nere leftshift
        {
            speed = sprintspeed; //�ka speed till sprintspeed
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) //om man sl�pper leftshift
        {
            speed = 12f; //�terst�ll speed
        }
    }
}

//made by tim