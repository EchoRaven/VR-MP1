using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Win Condition")]
    [SerializeField] private int totalGatesToUnlock = 3;
    private int currentUnlockedGates = 0;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI scoreboardText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    [Header("Timer Settings")]
    [SerializeField] private float gameTime = 300f; // 5 minutes
    [SerializeField] private bool useTimer = true;
    private float currentTime;
    private bool gameEnded = false;

    [Header("Collectibles")]
    [SerializeField] private TextMeshProUGUI collectiblesText;
    private int collectiblesCount = 0;

    [Header("Win Celebration")]
    [SerializeField] private GameObject[] winCelebrationObjects;
    [SerializeField] private Light[] celebrationLights;
    [SerializeField] private AudioSource winSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentTime = gameTime;
        UpdateScoreboard();
        UpdateCollectiblesUI();

        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);

        foreach (var obj in winCelebrationObjects)
        {
            if (obj != null) obj.SetActive(false);
        }
    }

    private void Update()
    {
        if (gameEnded || !useTimer) return;

        currentTime -= Time.deltaTime;
        UpdateTimerUI();

        if (currentTime <= 0)
        {
            currentTime = 0;
            LoseGame();
        }
    }

    public void UnlockGate()
    {
        if (gameEnded) return;

        currentUnlockedGates++;
        UpdateScoreboard();
        Debug.Log($"Gate unlocked! {currentUnlockedGates}/{totalGatesToUnlock}");

        if (currentUnlockedGates >= totalGatesToUnlock)
        {
            WinGame();
        }
    }

    public void LockGate()
    {
        if (gameEnded) return;

        currentUnlockedGates = Mathf.Max(0, currentUnlockedGates - 1);
        UpdateScoreboard();
    }

    public void CollectItem()
    {
        collectiblesCount++;
        UpdateCollectiblesUI();
    }

    private void UpdateScoreboard()
    {
        if (scoreboardText != null)
        {
            scoreboardText.text = $"Progress: {currentUnlockedGates} / {totalGatesToUnlock}";
        }
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = $"Time: {minutes:00}:{seconds:00}";
        }
    }

    private void UpdateCollectiblesUI()
    {
        if (collectiblesText != null)
        {
            collectiblesText.text = $"Collectibles: {collectiblesCount}";
        }
    }

    private void WinGame()
    {
        gameEnded = true;
        Debug.Log("You Win!");

        if (winPanel != null) winPanel.SetActive(true);

        foreach (var obj in winCelebrationObjects)
        {
            if (obj != null) obj.SetActive(true);
        }

        foreach (var light in celebrationLights)
        {
            if (light != null)
            {
                light.color = Color.green;
                light.intensity *= 2;
            }
        }

        if (winSound != null) winSound.Play();
    }

    private void LoseGame()
    {
        gameEnded = true;
        Debug.Log("Time's up! You Lose!");

        if (losePanel != null) losePanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public bool IsGameEnded() => gameEnded;
    public int GetCurrentProgress() => currentUnlockedGates;
}
