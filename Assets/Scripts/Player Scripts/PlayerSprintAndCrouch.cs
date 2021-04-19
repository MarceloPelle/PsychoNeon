using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    public float sprintSpeed = 10f;
    public float moveSpeed = 5f;
    public float crouchSpeed = 2f;
    public float fillVelocity;

    private Transform _lookRoot;
    private float _standHeight = 1.6f;
    private float _crouchHeight = 1f;

    private bool _is_Crouching;

    private PlayerStats playerStats;

    public float dashPower;
    private float _sprintValue = 100f;
    public float sprintTreshold = 10f;
    

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        playerStats = GetComponent<PlayerStats>();
        _lookRoot = transform.GetChild(0);
    }
  
    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
    }
    void Sprint()
    {
        //si tenemos stamina puede sprintar
        if(_sprintValue > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !_is_Crouching)
            {
                _playerMovement.speed = sprintSpeed;
            }
        }

        if(Input.GetKeyUp(KeyCode.LeftShift) && !_is_Crouching)
        {
            _playerMovement.speed = moveSpeed;
        }

        if(Input.GetKey(KeyCode.LeftShift) && !_is_Crouching)
        {
             if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                
            }
            _sprintValue -= sprintTreshold * Time.deltaTime;

            if(_sprintValue <= 0f)
            {
                _sprintValue = 0f;
                _playerMovement.speed = moveSpeed;
            }

            playerStats.DisplayStaminaStats(_sprintValue);

        }
        else
        {
            if(_sprintValue != 100f)
            {
                 //Stamina fill with fillvelocity and treshold
                _sprintValue += (sprintTreshold / 2) * Time.deltaTime * fillVelocity;

                playerStats.DisplayStaminaStats(_sprintValue);

                if(_sprintValue > 100f)
                {
                    _sprintValue = 100f;
                }

            }
        }

    }//sprint
 
    void Crouch()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            // si estamos agachados - Se para
            if(_is_Crouching)
            {
                CharacterController characterController = GetComponent<CharacterController>();
                characterController.height = 3f;

                _lookRoot.localPosition = new Vector3(0f, _standHeight, 0f);
                _playerMovement.speed = moveSpeed;

                _is_Crouching = false;

            }
            else
            {
                // si estamos no agachados - Se agacha

                CharacterController characterController = GetComponent<CharacterController>();
                characterController.height = 2.6f;

                _lookRoot.localPosition = new Vector3(0f, _crouchHeight, 0f);
                _playerMovement.speed = crouchSpeed;

                _is_Crouching = true;

            }
        }  
    }//Agacharse

}
