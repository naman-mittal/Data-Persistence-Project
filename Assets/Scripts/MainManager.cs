using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text HighScoreText;
    public Text ScoreText;
    public TMP_InputField nameField;
    public GameObject GameOverUI;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    private GameObject bricks;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Manager.scores.Count == 0)
        {
            HighScoreText.text = "Set a high score";
        }
        else
        {
            HighScoreText.text = $"High Score -> {GameManager.Manager.scores[0].name.ToUpper()} : {GameManager.Manager.scores[0].score}";
        }

        bricks = new GameObject();
        bricks.name = "bricks";

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {

                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.transform.SetParent(bricks.transform);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        Destroy(bricks);
        m_GameOver = true;
        GameOverUI.SetActive(true);
    }

    public void SaveScore()
    {
        GameManager.ScoreData data = new GameManager.ScoreData();

        if(nameField.text.Length == 0)
        {
            data.name = "AAA";
        }
        else
        {
            data.name = nameField.text;
        }
        
        
        data.score = m_Points;

        GameManager.Manager.SaveScore(data);
        SceneManager.LoadScene(2);
    }
}
