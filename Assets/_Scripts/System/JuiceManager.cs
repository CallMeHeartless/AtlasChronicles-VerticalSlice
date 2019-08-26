using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuiceManager {
    

    public static IEnumerator Shake(Transform _object, float _duration, float _magnitude)
    {
        Vector3 originalPosition = _object.localPosition;
        float elapsed = 0.0f;

        while(elapsed < _duration)
        {
            //If time elapsed is less than time to shake, keep shaking
            float x = Random.Range(-0.5f, 0.5f) * _magnitude;
            float y = Random.Range(-0.5f, 0.5f) * _magnitude;

            _object.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null; //Wait til next frame is drawn before calling while loop again
        }
        _object.localPosition = originalPosition;
    }
}
