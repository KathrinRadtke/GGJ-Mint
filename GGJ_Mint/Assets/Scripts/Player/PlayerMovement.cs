using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    public float m_MovementSpeed = 1;
    public Animator m_Animator;
    Vector3 inputMovement;

   // Hidden Variables
   [HideInInspector] public Rigidbody rb;
    
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovementInput();
        AnimateCharacter();
    }

    public void AnimateCharacter()
    {
        if (inputMovement != Vector3.zero) m_Animator.SetBool("isMoving", true);
        else m_Animator.SetBool("isMoving", false);

        m_Animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        m_Animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
    }

    public void MovementInput()
    {
        inputMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.Translate(inputMovement * Time.deltaTime * m_MovementSpeed, Space.World);
        SetWalkAnimation(inputMovement);
    }

    private void SetWalkAnimation(Vector3 inputMovement)
    {
        Debug.Log(inputMovement);
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
