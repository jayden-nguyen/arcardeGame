using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(ChangeSceneCoroutine());
	}
	
	IEnumerator ChangeSceneCoroutine()
    {
        yield return new WaitForSeconds(2);
        Camera.main.GetComponent<CameraMasker>().MaskChangeScene("Level1");
    }
}
