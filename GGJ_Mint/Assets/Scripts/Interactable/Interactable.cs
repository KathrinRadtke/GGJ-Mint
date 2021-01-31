using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{
    public enum InteractableType
    {
        TASK,
        ACTIVITY,
        BED
    }

    [Header("Interactable Settings")]
    public InteractableType m_Type;
    public float m_InteractableRange = 1.5f;

    private bool isInteractable = true;
    [SerializeField] private GameObject buttonPromt;
    [SerializeField] private GameObject sparcles;
    [SerializeField] private GameObject disableOnInteract;

    // Hidden Variable
    [HideInInspector] private Outline outline;

    public void SetOutline(bool active)
    {
        if (isInteractable)
        {
            outline.enabled = active;
        }
    }

    private void Start()
    {
        outline = GetComponent<Outline>();
    }

    public void Interact()
    {
        if (!isInteractable) return;
        if (m_Type == InteractableType.ACTIVITY)
        {
            InteractAnimation();
            GameFlowService.Instance.PlayActivity();
        } 
        else if(m_Type == InteractableType.TASK)
        {
            InteractAnimation();
            GameFlowService.Instance.PlayTask();
        }
        else
        {
            // InteractAnimation();
            GameFlowService.Instance.GoToBed();
        }

        if (disableOnInteract != null)
        {
            disableOnInteract.SetActive(false);
        }
        buttonPromt.SetActive(false);
        sparcles.SetActive(false);
    }

    public void EnableButtonPromt(bool active)
    {
        if (buttonPromt && isInteractable)
        {
            buttonPromt.SetActive(active);
            if (isInteractable)
            {
                sparcles.SetActive(!active);
            }
            else
            {
                sparcles.SetActive(false);
            }
        }
    }

    public void Disable()
    {
        isInteractable = false;
        outline.enabled = false;
        sparcles.SetActive(false);
    }

    private void InteractAnimation()
    {
        Vector3 oldScale = gameObject.transform.localScale;

        LeanTween.scale(gameObject, new Vector3(oldScale.x - oldScale.x / 10, oldScale.y + oldScale.y / 10 * 2, oldScale.z), 0.3f).setEaseInOutBack().setOnComplete(() => {
            LeanTween.scale(gameObject, oldScale, 0.3f).setEaseOutBounce();
        });
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(gameObject.transform.position, m_InteractableRange);
    }
}
