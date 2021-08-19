using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotationindicator : MonoBehaviour {

    private Camera _mainCamera;
    
    // Start is called before the first frame update
    void Start() {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        Vector2 positionOnScreen = _mainCamera.WorldToViewportPoint(transform.position);
        Vector3 mousePosition = (Vector2) _mainCamera.ScreenToViewportPoint(Input.mousePosition);
        float angle = AngleBetweenTwoPoints(positionOnScreen, mousePosition) + 90;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
    
    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
