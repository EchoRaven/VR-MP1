using UnityEngine;
using UnityEngine.InputSystem;


public class MouseGrabber : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float grabDistance = 5f;
    [SerializeField] private float holdDistance = 2f;
    [SerializeField] private float throwForce = 10f;

    [Header("References")]
    [SerializeField] private Camera mainCamera;

    private GameObject heldObject;
    private Rigidbody heldRigidbody;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable heldInteractable;
    private bool isHolding = false;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        var mouse = Mouse.current;
        if (mouse == null || mainCamera == null) return;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            if (isHolding)
            {
                ReleaseObject();
            }
            else
            {
                TryGrabObject();
            }
        }

        if (mouse.rightButton.wasPressedThisFrame && isHolding)
        {
            ThrowObject();
        }

        if (isHolding && heldObject != null)
        {
            MoveHeldObject();
        }

        // Scroll to adjust hold distance
        float scroll = mouse.scroll.ReadValue().y;
        if (scroll != 0)
        {
            holdDistance = Mathf.Clamp(holdDistance + scroll * 0.001f, 0.5f, 10f);
        }
    }

    private void TryGrabObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, grabDistance))
        {
            UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable = hit.collider.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            if (interactable != null && rb != null)
            {
                heldObject = hit.collider.gameObject;
                heldRigidbody = rb;
                heldInteractable = interactable;
                
                heldRigidbody.useGravity = false;
                heldRigidbody.linearDamping = 10f;
                heldRigidbody.angularDamping = 10f;
                
                isHolding = true;
                holdDistance = hit.distance;

                Debug.Log($"Grabbed: {heldObject.name}");
            }
        }
    }

    private void MoveHeldObject()
    {
        if (heldRigidbody == null) return;

        Vector3 targetPosition = mainCamera.transform.position + mainCamera.transform.forward * holdDistance;
        Vector3 direction = targetPosition - heldObject.transform.position;
        
        heldRigidbody.linearVelocity = direction * 15f;
    }

    private void ReleaseObject()
    {
        if (heldRigidbody != null)
        {
            heldRigidbody.useGravity = true;
            heldRigidbody.linearDamping = 0f;
            heldRigidbody.angularDamping = 0.05f;
            heldRigidbody.linearVelocity = Vector3.zero;

            Debug.Log($"Released: {heldObject.name}");
        }

        heldObject = null;
        heldRigidbody = null;
        heldInteractable = null;
        isHolding = false;
    }

    private void ThrowObject()
    {
        if (heldRigidbody != null)
        {
            Vector3 throwDirection = mainCamera.transform.forward;
            
            heldRigidbody.useGravity = true;
            heldRigidbody.linearDamping = 0f;
            heldRigidbody.angularDamping = 0.05f;
            heldRigidbody.linearVelocity = throwDirection * throwForce;

            Debug.Log($"Threw: {heldObject.name}");
        }

        heldObject = null;
        heldRigidbody = null;
        heldInteractable = null;
        isHolding = false;
    }

    public bool IsHolding() => isHolding;
    public GameObject GetHeldObject() => heldObject;
}
