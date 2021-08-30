using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ScaleSpriteToFillsOrthographicCamera : MonoBehaviour
{

    [Tooltip("If the camToMatch is not orthographic, this code will do nothing.")]
    [SerializeField] Camera cameraToMatch;

    private void Start()
    {
        
        if (cameraToMatch == null || !cameraToMatch.orthographic) { return; }

        transform.localScale = Vector3.one;
        Renderer renderer = GetComponent<Renderer>();
        Vector3 baseSize = renderer.bounds.size;
        Vector3 cameraSize = baseSize;
        cameraSize.y = cameraToMatch.orthographicSize * 2;
        cameraSize.x = cameraSize.y * cameraToMatch.aspect;

        Vector3 scale = cameraSize.ComponentDivide(baseSize);

        transform.localScale = scale;
    }
}
