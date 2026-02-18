using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class SocketInteractorFilter : MonoBehaviour, IXRSelectFilter
{
    [Header("Filter Settings")]
    [SerializeField] private string requiredTag = "Clue";
    [SerializeField] private GameObject specificObject;
    [SerializeField] private bool useSpecificObject = false;

    public bool canProcess => isActiveAndEnabled;

    public bool Process(UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor interactor, UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable interactable)
    {
        GameObject obj = interactable.transform.gameObject;

        if (useSpecificObject && specificObject != null)
        {
            return obj == specificObject;
        }

        if (!string.IsNullOrEmpty(requiredTag))
        {
            return obj.CompareTag(requiredTag);
        }

        return true;
    }

    public void SetSpecificObject(GameObject obj)
    {
        specificObject = obj;
        useSpecificObject = obj != null;
    }

    public void SetRequiredTag(string tag)
    {
        requiredTag = tag;
        useSpecificObject = false;
    }
}
