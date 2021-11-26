using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplayer : MonoBehaviour
{
    public GameObject scoreRowPrefab;

    // Start is called before the first frame update
    void Start()
    {
        int sno = 1;
        foreach (GameManager.ScoreData data in GameManager.Manager.scores)
        {
            GameObject newScore = Instantiate(scoreRowPrefab, transform);

            //Debug.Log(newScore.transform.Find("sno").GetComponent<Text>());

            //newScore.transform.GetComponentsInChildren<Text>()[0].text = sno.ToString();

            Debug.Log(newScore.transform.childCount);

            newScore.transform.Find("sno").GetComponent<TextMeshProUGUI>().text = sno.ToString();
            newScore.transform.Find("name").GetComponent<TextMeshProUGUI>().text = data.name;
            newScore.transform.Find("score").GetComponent<TextMeshProUGUI>().text = data.score.ToString();

            sno++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        int sno = 1;
        foreach(GameManager.ScoreData data in GameManager.Manager.scores)
        {
            GameObject newScore = Instantiate(scoreRowPrefab, transform);

            newScore.transform.Find("sno").GetComponent<Text>().text = sno.ToString();
            newScore.transform.Find("name").GetComponent<Text>().text = data.name;
            newScore.transform.Find("score").GetComponent<Text>().text = data.score.ToString();

            sno++;
        }

        foreach (GameManager.ScoreData data in GameManager.Manager.scores)
        {
            GameObject newScore = Instantiate(scoreRowPrefab, transform);

            newScore.transform.Find("sno").GetComponent<Text>().text = sno.ToString();
            newScore.transform.Find("name").GetComponent<Text>().text = data.name;
            newScore.transform.Find("score").GetComponent<Text>().text = data.score.ToString();

            sno++;
        }


    }
}
