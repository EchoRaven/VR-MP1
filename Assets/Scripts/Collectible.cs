using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool destroyOnCollect = true;
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float bobSpeed = 2f;
    [SerializeField] private float bobHeight = 0.1f;

    [Header("Audio")]
    [SerializeField] private AudioSource collectSound;

    private Vector3 startPosition;
    private bool isCollected = false;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (isCollected) return;

        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCollected) return;

        if (other.CompareTag("Player") || other.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor>() != null)
        {
            Collect();
        }
    }

    public void Collect()
    {
        if (isCollected) return;

        isCollected = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.CollectItem();
        }

        if (collectSound != null)
        {
            collectSound.Play();
            if (destroyOnCollect)
            {
                Destroy(gameObject, collectSound.clip.length);
            }
        }
        else if (destroyOnCollect)
        {
            Destroy(gameObject);
        }
    }
}
