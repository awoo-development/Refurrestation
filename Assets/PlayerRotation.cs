using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Transform CharacterTransform;
    public float RotationSmoothingCoef = 0.01f;

    private Quaternion targetRotation;

    void Update()
    {
        var groundPlane = new Plane(Vector3.up, -CharacterTransform.position.y);
        var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDistance;

        if (groundPlane.Raycast(mouseRay, out hitDistance))
        {
            var lookAtPosition = mouseRay.GetPoint(hitDistance);
            targetRotation = Quaternion.LookRotation(lookAtPosition - CharacterTransform.position, Vector3.up);
        }
    }

    void FixedUpdate()
    {
        var rotation = Quaternion.Lerp(CharacterTransform.rotation, targetRotation, RotationSmoothingCoef);
        CharacterTransform.rotation = rotation;
    }
}
