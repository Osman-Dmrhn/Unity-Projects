using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float touchSensitivity=100f;
    public Transform player,cameraTransform;

    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.position.x > Screen.width / 2) // Sadece sað ekrandan giriþ al
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    float touchX = touch.deltaPosition.x * touchSensitivity * Time.deltaTime;
                    float touchY = touch.deltaPosition.y * touchSensitivity * Time.deltaTime;

                    // Kamera dönüþünü ayarla
                    xRotation -= touchY;
                    xRotation = Mathf.Clamp(xRotation, -90f, 90f);
                    cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

                    // Oyuncu gövdesini döndür
                    player.Rotate(Vector3.up * touchX);
                }
            }
        }
    }
}
