using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private float interactionRange = 3f;
    [SerializeField]
    private LayerMask interactionLayer;

    private enum InteractionType
    {
        None,
        Circle,
        Box
    }
    [SerializeField]
    private InteractionType currentInteraction = InteractionType.None;

    private IInteractable currentInteractable;
    [SerializeField]
    private GameObject interactionArrowObject;

    void Update()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, interactionRange, Vector3.up,0,interactionLayer);
        float closestDistance = float.MaxValue;
        foreach (var hit in hits)
        {
            float distance = Vector3.Distance(transform.position, hit.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                currentInteractable = hit.transform.GetComponent<IInteractable>();
                interactionArrowObject.SetActive(true);
                interactionArrowObject.transform.position = hit.transform.position + Vector3.up * 2;
            }
        }

        if(hits.Length == 0)
        {
            interactionArrowObject.SetActive(false);
            currentInteractable = null;
        }

        if (Input.GetKeyDown(KeyManager.Instance.GetkeyCode(KeyType.Interaction)))
        {
            Interact();
        }
    }

    private void Interact()
    {
        currentInteractable?.Interact();
    }

    void OnDrawGizmos()
    {
        

        if (currentInteraction == InteractionType.None)
        {
            return;
        }

        Gizmos.color = Color.green;
        if (currentInteraction == InteractionType.Circle)
        {
            Gizmos.DrawWireSphere(transform.position, interactionRange);
        }
        else if (currentInteraction == InteractionType.Box)
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(interactionRange, interactionRange, interactionRange));
        }

        
    }
}
