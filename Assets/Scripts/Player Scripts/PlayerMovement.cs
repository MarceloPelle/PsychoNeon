using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;

    private PlayerSprintAndCrouch dashManagment;

    private PlayerStats playerStats;

    Rigidbody rb;

    public Vector3 movementVector = Vector3.zero;

    public float speed = 5f;

    public float caida; 

    [SerializeField]
    private float _gravity = 20f;

    public float jumpForce = 10f;
    private float _verticalVelocity;

    

    //Dash
    [SerializeField]
    private float dashSpeed;
    
    [SerializeField]
    private float dashTime;

    [SerializeField]
    private float dashStaminaDown;

    public float dashCDEntry;
    public float dashCD;



    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        

    }
  
    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }

        if(Input.GetKeyDown(KeyCode.F5))
        {
            RestartGame();
        }

        MoveThePlayer();

        dashCDEntry -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
    
            if (dashCDEntry <= 0)
            {
                StartCoroutine(Dash());
            }
        }
    }

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            GetComponent<Collider>().attachedRigidbody.AddForce(movementVector, ForceMode.Impulse);
            
        }
        
    }
    void MoveThePlayer()
    {

        movementVector = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f,
                                     Input.GetAxis(Axis.VERTICAL));

        movementVector = transform.TransformDirection(movementVector);
        movementVector *= speed * Time.deltaTime;

        ApplyGravity();

        _characterController.Move(movementVector);

    }// move player
    void ApplyGravity()
    {
        if (_characterController.isGrounded)
        {
            _verticalVelocity -= _gravity * Time.deltaTime;

            //jump
            PlayerJump();
        }
        else
        {
            
            _verticalVelocity -= _gravity * Time.deltaTime * +caida;
        }

        movementVector.y = _verticalVelocity * Time.deltaTime;

    }// aplicar gravedad
    void PlayerJump()
    {
        if(_characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _verticalVelocity = jumpForce;
        }
    }// Salto del jugador
    IEnumerator Dash()
    {
        float startTime = Time.time;


        while(Time.time < startTime + dashTime)
        {
            _characterController.Move(movementVector * dashSpeed * Time.deltaTime);
            dashCDEntry = dashCD;

            yield return null;
        }
    }//Dash

    private void ExitGame()
    {
        Application.Quit();
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
