using UnityEngine;
using UnityEngine.Events;

public class ClickableButton : MonoBehaviour
{
    public UnityEvent onClick;

    private void OnMouseDown()
    {
        onClick?.Invoke();
    }

    // Also detect collision/trigger for VR
    private void OnTriggerEnter(Collider other)
    {
        onClick?.Invoke();
    }

    // Called by raycast from MouseGrabber
    public void Click()
    {
        Debug.Log($"Button clicked: {gameObject.name}");
        onClick?.Invoke();
    }
}
