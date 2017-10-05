using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameplayManager : MonoBehaviour {
	public static GameplayManager Instance { get; private set; }
    public List<SpriteRenderer> WallStr;
	public string nextLevelName;
    private Color originalColor;
	private List<BlockController> allBlocks = new List<BlockController>();
	private bool isGameOver = false;
    private int consecutiveHit = 0;
    public Text ScoreText;


	void Awake() {
		Instance = this;
        originalColor = WallStr[0].color;
        ScoreText.text = Database.Instance.scoredata.score.ToString();
	}
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    public void OnBallHitBottom(){
		Camera.main.GetComponent<CameraMasker> ().MaskChangeScene ("Gameover");
		isGameOver = true;
	}

	public void OnNewBlockCreated(BlockController block){
		allBlocks.Add (block);
	}

	public void OnBlockDestroyed(BlockController block){
		allBlocks.Remove (block);
		if (allBlocks.Count == 0 && !isGameOver) {
			LevelCompleted ();
		}
	}
    public void WallFlash()
    {
        if (WallFlashCoroutine() != null)
        {
            StopCoroutine(WallFlashCoroutine());
        }
        StartCoroutine(WallFlashCoroutine());
    }
    IEnumerator WallFlashCoroutine()
    {
        Color StartColor = Color.HSVToRGB(Random.Range(0f,1f),0.9f,0.9f);
        Color originalColor = WallStr[0].color;

        float time = 0;
        while (time < 0.3f)
        {
            WallStr.ForEach(sr =>
            {
                sr.color = Color.Lerp(StartColor, originalColor, time / 0.3f);
            });
            
            time += Time.deltaTime;
            yield return null;
        }
        //foreach (SpriteRenderer sr in WallStr)
        //{
        //    sr.color = originalColor;
        //}
        WallStr.ForEach(sr=> 
        {
            sr.color = originalColor;
        });
    }
	private void LevelCompleted(){
		Camera.main.GetComponent<CameraMasker> ().MaskChangeScene (nextLevelName);
	}
    public void OnBallHitBlock()
    {
        consecutiveHit++;
        Database.Instance.scoredata.IncreaseScore( consecutiveHit);
        ScoreText.text = Database.Instance.scoredata.score.ToString();
    }
    public void OnBallHitPaddle()
    {
        consecutiveHit = 0;
    }
}
