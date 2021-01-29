using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    public float m_MovementSpeed = 1;

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
        Vector3 inputMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.Translate(inputMovement * Time.deltaTime * m_MovementSpeed, Space.World);
    }
}
