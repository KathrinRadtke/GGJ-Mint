using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum InteractableType
    {
        TASK,
        ACTIVITY
    }

    [Header("Interactable Settings")]
    public InteractableType m_Type;
    public float m_InteractableRange = 1.5f;

    public void Interact()
    {
        if (m_Type == InteractableType.ACTIVITY)
        {
            InteractAnimation();
            GameFlowService.Instance.PlayActivity();
        } else
        {
            InteractAnimation();
            GameFlowService.Instance.PlayTask();
        }
    }

    private void InteractAnimation()
    {
        LeanTween.scale(gameObject, new Vector3(0.7f, 1.3f, 1), 0.3f).setEaseInOutBack().setOnComplete(() => {
            LeanTween.scale(gameObject, Vector3.one, 0.3f).setEaseOutBounce();
        });
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(gameObject.transform.position, m_InteractableRange);
    }
}
