using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    [Header("Door Settings")]
    [SerializeField] private Transform doorPivot;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 2f;
    [SerializeField] private bool openOnStart = false;

    [Header("Audio")]
    [SerializeField] private AudioSource doorSound;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen = false;
    private bool isAnimating = false;

    private void Start()
    {
        if (doorPivot == null) doorPivot = transform;

        closedRotation = doorPivot.localRotation;
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);

        if (openOnStart)
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        if (isOpen || isAnimating) return;
        StartCoroutine(AnimateDoor(openRotation, true));
    }

    public void CloseDoor()
    {
        if (!isOpen || isAnimating) return;
        StartCoroutine(AnimateDoor(closedRotation, false));
    }

    public void ToggleDoor()
    {
        if (isOpen)
            CloseDoor();
        else
            OpenDoor();
    }

    private IEnumerator AnimateDoor(Quaternion targetRotation, bool opening)
    {
        isAnimating = true;

        if (doorSound != null)
        {
            doorSound.Play();
        }

        float elapsed = 0f;
        Quaternion startRotation = doorPivot.localRotation;

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * openSpeed;
            doorPivot.localRotation = Quaternion.Slerp(startRotation, targetRotation, elapsed);
            yield return null;
        }

        doorPivot.localRotation = targetRotation;
        isOpen = opening;
        isAnimating = false;
    }

    public bool IsOpen() => isOpen;
}
