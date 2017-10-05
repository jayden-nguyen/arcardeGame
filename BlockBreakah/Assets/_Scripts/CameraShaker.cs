using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour {
    public float shakeMagnitude;
    private Coroutine shakeCoroutine;
	void Shake(Vector2 direction)
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);

        }
        shakeCoroutine = StartCoroutine(ShakeCoroutine(-direction.ScaleTo(shakeMagnitude)));
    }
    IEnumerator ShakeCoroutine(Vector2 shakeDirection)
    {
        float time = 0;
        while(time < 0.3f)
        {
            transform.localPosition = Vector2.Lerp(shakeDirection, Vector2.zero, Mathfx.Bounce(time / 0.3f));
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = Vector2.zero;
    }
}
