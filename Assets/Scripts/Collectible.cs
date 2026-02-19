using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool destroyOnCollect = true;
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float bobSpeed = 2f;
    [SerializeField] private float bobHeight = 0.1f;
    [SerializeField] private float collectDistance = 1.5f;

    [Header("Audio")]
    [SerializeField] private AudioSource collectSound;

    private Vector3 startPosition;
    private bool isCollected = false;
    private Transform playerTransform;

    private void Start()
    {
        startPosition = transform.position;
        
        // Find player camera
        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            playerTransform = mainCam.transform;
        }
    }

    private void Update()
    {
        if (isCollected) return;

        // Rotate and bob animation
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Check distance to player
        if (playerTransform != null)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            if (distance < collectDistance)
            {
                Collect();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCollected) return;
        Collect();
    }

    public void Collect()
    {
        if (isCollected) return;

        isCollected = true;
        Debug.Log($"Collected: {gameObject.name}");

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
