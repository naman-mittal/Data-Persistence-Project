using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Manager;

    public List<ScoreData> scores;

    private void Awake()
    {
        if (Manager != null)
        {
            Destroy(gameObject);
            return;
        }
        Manager = this;
        DontDestroyOnLoad(gameObject);
        LoadScores();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Serializable]
    public class SaveData
    {
        public ScoreData[] scores;
    }

    [System.Serializable]
    public class ScoreData
    {
        public string name;
        public int score;
    }

    public void SaveScore(ScoreData score)
    {
        string path = Application.persistentDataPath + "/saveFile.json";

        SaveData data = new SaveData();

        AddScore(score);

        data.scores = scores.ToArray();

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(path, json);

    }

    private void AddScore(ScoreData scoreData)
    {

        int index = scores.FindIndex(score => score.name.Equals(scoreData.name));

        if (index >= 0)
        {
            if (scoreData.score <= scores[index].score)
                return;

            scores.RemoveAt(index);
        }

        

        

        int indexToAdd = -1;

        for(int i = 0; i < scores.Count; i++)
        {
            if(scoreData.score >= scores[i].score)
            {
                indexToAdd = i;
                break;
            }
        }

        if(indexToAdd == -1)
        {
            scores.Add(scoreData);
        }
        else
        {
            scores.Insert(indexToAdd, scoreData);
        }

        


    }

    public void LoadScores()
    {
        string path = Application.persistentDataPath + "/saveFile.json";
        Debug.Log(path);

        if (File.Exists(path))
        {
            SaveData data = JsonUtility.FromJson<SaveData>(File.ReadAllText(path));

            scores = new List<ScoreData>(data.scores);
        }

        

    }
}
