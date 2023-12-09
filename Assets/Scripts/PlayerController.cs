using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    private Animator anim;
    [Space]
    [Header("Player Controller")]
    [SerializeField] private float _movementSpeed=300;
    [SerializeField] private float _rotationSpeed = 500;
    private bool isRunnig;

    [Space]
    [Header("Joystick Controller")]
    [SerializeField] Joystick joystick;   
    float vertical, horizontal; 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (isRunnig)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }
    private void FixedUpdate()
    {
        JoystickMove();   
    }
    public void JoystickMove()
    {
       
        vertical = joystick.Vertical * _movementSpeed * Time.fixedDeltaTime;
        horizontal = joystick.Horizontal * _movementSpeed * Time.fixedDeltaTime;

        rb.velocity = new Vector3(horizontal, 0, vertical);
        if (rb.velocity != Vector3.zero)
        {
            isRunnig = true;
            Quaternion temp = Quaternion.LookRotation(rb.velocity, Vector3.up);
            rb.rotation = Quaternion.RotateTowards(transform.rotation, temp, _rotationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            isRunnig = false;
        }
    }
}