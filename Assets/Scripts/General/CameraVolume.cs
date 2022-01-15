using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CameraVolume : MonoBehaviour
{
    [SerializeField] float sizeTarget;
    [Range(0f, 1f)] [SerializeField] float lerpSpeed;
    private float sizeToBeLerped;
    float originalSize;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        originalSize = Camera.main.orthographicSize;
        Camera.main.GetComponent<CameraController>().LerpToCameraSize(sizeTarget, lerpSpeed);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        Camera.main.GetComponent<CameraController>().LerpToCameraSize(originalSize, lerpSpeed);
    }

/*    private IEnumerator LerpCameraToSize(float target)
    {
        Debug.Log("Lerping!");
        sizeToBeLerped = originalSize;
        while (sizeToBeLerped != target)
        {
            sizeToBeLerped = Mathf.Lerp(sizeToBeLerped, target, lerpSpeed);
            Camera.main.orthographicSize = sizeToBeLerped;
            yield return new WaitForEndOfFrame();

        }

    }*/
}
