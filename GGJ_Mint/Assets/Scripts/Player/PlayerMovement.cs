using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    public float m_MovementSpeed = 1;
    public Animator m_Animator;
    Vector3 inputMovement;
    public GameObject GFX;

   // Hidden Variables
   [HideInInspector] public Rigidbody rb;
    
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovementInput();
    }

    public void MovementInput()
    {
        inputMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(inputMovement * Time.deltaTime * m_MovementSpeed, Space.World);
        
        if (inputMovement != Vector3.zero) transform.rotation = Quaternion.LookRotation(inputMovement);

        SetWalkAnimation();
    }

    private void SetWalkAnimation()
    {
        if (inputMovement != Vector3.zero)
        {
            m_Animator.SetBool("isWalking", true);
        }
        else
        {
            m_Animator.SetBool("isWalking", false);
        }
    }
}
