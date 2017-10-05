using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverManager : MonoBehaviour {
    public Text FinalScore;
    public static int highscore;
    public Text HighScore;
    public Text BlockDestroyed;
    private void Awake()
    {
        if(Database.Instance.scoredata.score >= Database.Instance.scoredata.highscore)
        {
            Database.Instance.scoredata.highscore = Database.Instance.scoredata.score;
        }
        FinalScore.text = Database.Instance.scoredata.score.ToString();
        HighScore.text = Database.Instance.scoredata.highscore.ToString();
        BlockDestroyed.text = "Block Destroyed: "+Database.Instance.achievementdata.BlockDestroyed.ToString();
        Database.Instance.Save();
    }
    public void PlayAgain(){		
        Database.Instance.scoredata.ResetScore();
        SceneManager.LoadScene("Level1");
    }
}
