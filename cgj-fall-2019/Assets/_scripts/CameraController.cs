using System.Collections;
using UnityEngine;

public class CameraController : GameBehaviour
{
    public void Shake(float duration = 0.5f, float shakeAmount = 0.2f)
    {
        StartCoroutine(DoShake(duration, shakeAmount));
    }

    private IEnumerator DoShake(float duration, float shakeAmount)
    {
        var time = 0f;
        var position = transform.localPosition;
        while (time < duration)
        {
            transform.localPosition = position + Random.insideUnitSphere * shakeAmount;
            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = position;
    }

}
