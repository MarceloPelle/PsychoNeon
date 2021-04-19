using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private Transform playerRoot, lookRoot;

    [SerializeField]
    private bool invert;
    private bool can_Unclok = true;

    [SerializeField]
    private float _sensivity = 5f;

    [SerializeField]
    private int _smooth_Steps = 10;

    [SerializeField]
    private float _smooth_Weight = 0.4f;

    [SerializeField]
    private float _roll_Angle = 10f;

    [SerializeField]
    private float _roll_Speed = 3f;

    [SerializeField]
    private Vector2 _default_Look_Limits = new Vector2(-70, 80);

    private Vector2 _look_Angles;

    private Vector2 _current_Mouse_Look;
    private Vector2 smooth_Move;

    private float _current_Roll_Angle;

    private int last_Look_Frame;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
    }

    // Update is called once per frame
    void Update()
    {
        LockAndUnlockCursor(); // Para hacer el cambio entre menu y juego

        if(Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
    }
    void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
             if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }// bloquear y desbloquar

    void LookAround()
    {
        _current_Mouse_Look = new Vector2(
            Input.GetAxis(MouseAxis.MOUSE_Y), Input.GetAxis(MouseAxis.MOUSE_X));

        _look_Angles.x += _current_Mouse_Look.x * _sensivity * (invert ? 1f : -1f);
        _look_Angles.y += _current_Mouse_Look.y * _sensivity;

        _look_Angles.x = Mathf.Clamp(_look_Angles.x, _default_Look_Limits.x, _default_Look_Limits.y);

        // hacer estado de borracho
        //if(drunk)
        //{

        //_current_Roll_Angle =
        //    Mathf.Lerp(_current_Roll_Angle, Input.GetAxisRaw(TagHolder.MouseAxis.MOUSE_X)
        //                * _roll_Angle, Time.deltaTime * _roll_Speed);
        //}
        

        lookRoot.localRotation = Quaternion.Euler(_look_Angles.x, 0f, _current_Roll_Angle);
        playerRoot.localRotation = Quaternion.Euler(0f, _look_Angles.y, 0f);


    } //look around
}
