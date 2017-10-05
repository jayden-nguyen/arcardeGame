using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class Database : MonoBehaviour {
    public static Database Instance { get; private set; }
    public ScoreData scoredata = new ScoreData();
    public AchievementData achievementdata = new AchievementData();
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Load();
        }else
        {
            Destroy(gameObject);
        }
        //PlayerPrefs.SetInt("highScore", scoredata.highscore);
        //PlayerPrefs.GetInt("highScore");

    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream file = File.Create(Application.persistentDataPath + "/score.dat")) {
            bf.Serialize(file, scoredata);
        }
            ;
        BinaryFormatter block = new BinaryFormatter();
        using (FileStream AchieFile = File.Create(Application.persistentDataPath + "/achievement.dat"))
        {
            block.Serialize(AchieFile, achievementdata);
        }
    }
    public void Load()
    {

        BinaryFormatter bf = new BinaryFormatter();
        try
        {
            using (FileStream file = File.Open(Application.persistentDataPath + "/score.dat", FileMode.Open))
            {
                scoredata = bf.Deserialize(file) as ScoreData;
            }
        }
        catch
        {
            scoredata = new ScoreData();
        }
        scoredata.ResetScore();

        BinaryFormatter block = new BinaryFormatter();
        try
        {
            using (FileStream AchieFile = File.Open(Application.persistentDataPath + "/achievement.dat", FileMode.Open))
            {
                achievementdata = block.Deserialize(AchieFile) as AchievementData;
            }
        }
        catch
        {
            achievementdata = new AchievementData();
        }
        achievementdata.ResetBlockDestroyed();
    }
}
[System.Serializable]
public class ScoreData
{
    public int score = 0;
    public int highscore = 0;
    public void IncreaseScore(int amount)
    {
        if(amount > 0)
        {
            score += amount;

        }
    }
    public void ResetScore()
    {
        score = 0;
    }
}
[System.Serializable]
public class AchievementData
{
    public int BlockDestroyed = 0;
    public void IncreaseBlockDestroyed()
    {
        BlockDestroyed += 1;
    }
    public void ResetBlockDestroyed()
    {
        BlockDestroyed = 0;
    }
}