using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("プレハブ")]
    public GameObject[] packagePrefabs;
    public GameObject[] deliveryPrefabs;

    [Header("スポーン地点")]
    public Transform[] packageSpawnPoints;
    public Transform[] deliverySpawnPoints;

    [Header("UI")]
    public Text scoreText;
    public Text timerText;

    [Header("ゲームオーバー")]
    public GameObject gameOverPanel;
    public Text resultText;

    [Header("タイマー")]
    public float gameTime = 60f;

    private bool isGameOver = false;

    private GameObject currentPackage;
    private GameObject currentDelivery;

    public int score = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateScoreUI();

        if (timerText != null)
        {
            timerText.text = "Time : " + Mathf.CeilToInt(gameTime);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        SpawnDeliverySet();
    }

    private void Update()
    {
        if (isGameOver)
            return;

        gameTime -= Time.deltaTime;

        if (timerText != null)
        {
            timerText.text = "Time : " + Mathf.CeilToInt(gameTime);
        }

        if (gameTime <= 0)
        {
            GameOver();
        }
    }

    public void SpawnDeliverySet() { if (isGameOver) return; int colorIndex = Random.Range(0, packagePrefabs.Length); Transform packageSpawn = packageSpawnPoints[Random.Range(0, packageSpawnPoints.Length)]; Transform deliverySpawn = deliverySpawnPoints[Random.Range(0, deliverySpawnPoints.Length)]; currentPackage = Instantiate(packagePrefabs[colorIndex], packageSpawn.position, Quaternion.identity); currentDelivery = Instantiate(deliveryPrefabs[colorIndex], deliverySpawn.position, Quaternion.identity); }

    public void DeliveryCompleted()
    {
        Destroy(currentPackage);
        Destroy(currentDelivery);

        SpawnDeliverySet();
    }

    public void AddScore(int amount)
    {
        score += amount;

        UpdateScoreUI();

        Debug.Log("現在のスコア : " + score);
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score : " + score;
        }
    }

    private void GameOver()
    {
        isGameOver = true;

        Debug.Log("ゲーム終了");

        if (timerText != null)
        {
            timerText.text = "Time : 0";
        }

        if (currentPackage != null)
        {
            Destroy(currentPackage);
        }

        if (currentDelivery != null)
        {
            Destroy(currentDelivery);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (resultText != null)
        {
            resultText.text = "Score : " + score;
        }
    }

    public void SetCurrentPackage(GameObject package)
    {
        currentPackage = package;
    }
}