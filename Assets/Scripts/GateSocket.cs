using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GateSocket : MonoBehaviour
{
    [Header("Socket Settings")]
    [SerializeField] private string acceptedClueTag = "Clue";
    [SerializeField] private bool requireSpecificClue = false;
    [SerializeField] private GameObject specificClue;

    [Header("Secret/Door Settings")]
    [SerializeField] private GameObject secretToReveal;
    [SerializeField] private GameObject obstructionToRemove;
    [SerializeField] private Transform doorToOpen;
    [SerializeField] private Vector3 doorOpenRotation = new Vector3(0, 90, 0);

    [Header("Visual Feedback")]
    [SerializeField] private Light socketLight;
    [SerializeField] private Color unlockedColor = Color.green;
    [SerializeField] private Color lockedColor = Color.red;

    [Header("Audio")]
    [SerializeField] private AudioSource unlockSound;

    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socketInteractor;
    private bool isUnlocked = false;

    private void Awake()
    {
        socketInteractor = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();

        if (socketInteractor == null)
        {
            Debug.LogError("GateSocket requires an XRSocketInteractor component!");
            return;
        }

        socketInteractor.selectEntered.AddListener(OnObjectPlaced);
        socketInteractor.selectExited.AddListener(OnObjectRemoved);
    }

    private void Start()
    {
        if (socketLight != null)
        {
            socketLight.color = lockedColor;
        }
    }

    private void OnDestroy()
    {
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.RemoveListener(OnObjectPlaced);
            socketInteractor.selectExited.RemoveListener(OnObjectRemoved);
        }
    }

    private void OnObjectPlaced(SelectEnterEventArgs args)
    {
        GameObject placedObject = args.interactableObject.transform.gameObject;

        if (!IsCorrectClue(placedObject))
        {
            Debug.Log("Wrong clue placed in socket!");
            return;
        }

        UnlockGate();
    }

    private void OnObjectRemoved(SelectExitEventArgs args)
    {
        if (isUnlocked)
        {
            LockGate();
        }
    }

    private bool IsCorrectClue(GameObject obj)
    {
        if (requireSpecificClue)
        {
            return obj == specificClue;
        }

        return obj.CompareTag(acceptedClueTag);
    }

    private void UnlockGate()
    {
        if (isUnlocked) return;

        isUnlocked = true;
        Debug.Log($"Gate {gameObject.name} unlocked!");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.UnlockGate();
        }

        if (socketLight != null)
        {
            socketLight.color = unlockedColor;
        }

        if (unlockSound != null)
        {
            unlockSound.Play();
        }

        if (secretToReveal != null)
        {
            secretToReveal.SetActive(true);
        }

        if (obstructionToRemove != null)
        {
            obstructionToRemove.SetActive(false);
        }

        if (doorToOpen != null)
        {
            doorToOpen.Rotate(doorOpenRotation);
        }
    }

    private void LockGate()
    {
        isUnlocked = false;
        Debug.Log($"Gate {gameObject.name} locked again!");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.LockGate();
        }

        if (socketLight != null)
        {
            socketLight.color = lockedColor;
        }
    }

    public bool IsUnlocked() => isUnlocked;
}
