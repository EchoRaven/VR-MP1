using UnityEngine;
using UnityEngine.Events;


public class VRButton : MonoBehaviour
{
    [Header("Button Settings")]
    [SerializeField] private float pressDepth = 0.02f;
    [SerializeField] private float resetSpeed = 5f;

    [Header("Events")]
    public UnityEvent onButtonPressed;

    [Header("Audio")]
    [SerializeField] private AudioSource pressSound;

    private Vector3 startPosition;
    private bool isPressed = false;
    private float pressedTime = 0f;
    private float cooldownTime = 0.5f;

    private void Start()
    {
        startPosition = transform.localPosition;
    }

    private void Update()
    {
        if (isPressed)
        {
            pressedTime += Time.deltaTime;
            if (pressedTime >= cooldownTime)
            {
                isPressed = false;
                pressedTime = 0f;
            }
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, Time.deltaTime * resetSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPressed) return;

        if (other.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor>() != null || 
            other.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRPokeInteractor>() != null)
        {
            PressButton();
        }
    }

    private void PressButton()
    {
        isPressed = true;
        pressedTime = 0f;

        transform.localPosition = startPosition - new Vector3(0, pressDepth, 0);

        if (pressSound != null)
        {
            pressSound.Play();
        }

        onButtonPressed?.Invoke();
    }
}
