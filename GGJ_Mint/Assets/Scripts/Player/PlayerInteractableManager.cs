using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractableManager : MonoBehaviour
{
    [HideInInspector] private Interactable[] m_InteractableArray;
    [HideInInspector] private Interactable m_NearestInteractable;

    private void Start()
    {
        // Don't sue me for this garbage line
        m_InteractableArray = GameObject.FindObjectsOfType<Interactable>();
    }

    void Update()
    {
        getNearestInteractable();

        if (Input.GetKeyDown(KeyCode.E) && isInteractableInRange())
        {
            // TODO: Show Outline Shader
            m_NearestInteractable.Interact();
        }
    }

    private bool isInteractableInRange()
    {
        if (Vector3.Distance(m_NearestInteractable.gameObject.transform.position, transform.position) < m_NearestInteractable.m_InteractableRange)
            return true;
        return false;
    }

    private void getNearestInteractable()
    {
        foreach (Interactable inter in m_InteractableArray)
        {
            if (m_NearestInteractable == null) m_NearestInteractable = inter;
            else
            {
                if (inter == m_NearestInteractable) continue;

                if (Vector3.Distance(inter.gameObject.transform.position, transform.position) < Vector3.Distance(m_NearestInteractable.gameObject.transform.position, transform.position))
                {
                    m_NearestInteractable = inter;
                }
            }
        }
    }
}
