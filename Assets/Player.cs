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
        if (hit.gameObject.tag == "Lava") //om den träffar objekt med tagen lava
        {
            TakeDamage(0.08f);  //funktion takedamage
            Debug.Log("touched the lava");  //skriv i logs att det nuddade
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;     //när spelet börjar sätts hp till fullt
        //healthbar.SetMaxHealth(maxHealth);
    }

    void TakeDamage(float damage) //funktion som heter takedamage
    {
        currentHealth -= damage;
        //healthbar.SetHealth(currentHealth);
    }

    void die() //funktion som är att dö
    {
        Cursor.lockState = CursorLockMode.Confined; //låser upp muspekaren igen
        SceneManager.LoadScene(2); //ladda game over scene
    }
    // Update is called once per frame
    void Update()
    {

        if (currentHealth <= 0) //om hp är mindre än ELLER LIKA MED 0
        {
            die(); //kör funktionen die
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //checkar om den är grounded eller inte (beroende på groundCheck)

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

        if (Input.GetKeyDown(KeyCode.LeftShift)) //om man håller nere leftshift
        {
            speed = sprintspeed; //öka speed till sprintspeed
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) //om man släpper leftshift
        {
            speed = 12f; //återställ speed
        }
    }
}

//made by tim