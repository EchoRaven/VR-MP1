using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClueHighlight : MonoBehaviour
{
    [Header("Highlight Settings")]
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Color highlightColor = Color.yellow;
    [SerializeField] private float highlightIntensity = 1.5f;

    [Header("Outline Effect")]
    [SerializeField] private bool useOutline = true;
    [SerializeField] private Color outlineColor = Color.white;
    [SerializeField] private float outlineWidth = 0.02f;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private Renderer objectRenderer;
    private Material originalMaterial;
    private bool isHighlighted = false;

    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }

        if (grabInteractable != null)
        {
            grabInteractable.hoverEntered.AddListener(OnHoverEnter);
            grabInteractable.hoverExited.AddListener(OnHoverExit);
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.hoverEntered.RemoveListener(OnHoverEnter);
            grabInteractable.hoverExited.RemoveListener(OnHoverExit);
            grabInteractable.selectEntered.RemoveListener(OnGrab);
            grabInteractable.selectExited.RemoveListener(OnRelease);
        }
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        SetHighlight(true);
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        SetHighlight(false);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        SetHighlight(false);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        SetHighlight(false);
    }

    private void SetHighlight(bool highlight)
    {
        if (objectRenderer == null) return;

        isHighlighted = highlight;

        if (highlight)
        {
            if (highlightMaterial != null)
            {
                objectRenderer.material = highlightMaterial;
            }
            else
            {
                objectRenderer.material.SetColor("_EmissionColor", highlightColor * highlightIntensity);
                objectRenderer.material.EnableKeyword("_EMISSION");
            }
        }
        else
        {
            if (originalMaterial != null)
            {
                objectRenderer.material = originalMaterial;
            }
            objectRenderer.material.DisableKeyword("_EMISSION");
        }
    }
}
