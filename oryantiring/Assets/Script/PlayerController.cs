using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    //DE���KENLER
    public GameObject oyuncu;
    [SerializeField]public Joystick joystick;
    CharacterController _charCont;
    public float Hiz = 5.0f;
    public float ziplamaGucu = 20.0f;
    public float _gravity = 1.0f;
    Vector3 velocity;
    float y_velocity = 0.0f;
    bool yerdeMi;
    public float touchSensitivity = 100f;
    public Transform player, cameraTransform;
    float xRotation = 0f;

    void Start()
    {
        _charCont = oyuncu.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        Vector3 direction = new Vector3(horizontal, 0, vertical);


        direction = player.transform.TransformDirection(direction);

        if (direction.magnitude > 0.1f)
        {
            velocity = direction * Hiz;
        }
        else
        {
            velocity = Vector3.zero;
        }

        // Yer kontrolü
        if (_charCont.isGrounded)
        {
            Debug.Log("Yerde");
            if (Input.GetButtonDown("Jump"))
            {
                y_velocity = ziplamaGucu;
            }
        }
        else
        {
            y_velocity -= _gravity * Time.deltaTime;
        }

        // Hareketi uygula
        velocity.y = y_velocity;
        _charCont.Move(velocity * Time.deltaTime);



        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                // Sadece sağ ekrandan gelen ve UI elemanına dokunmayan parmaklarla kamera kontrolü
                if (touch.phase == TouchPhase.Moved &&
                    touch.position.x > Screen.width / 2 &&
                    !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    float touchX = touch.deltaPosition.x * touchSensitivity * 2.5f * Time.deltaTime;
                    float touchY = touch.deltaPosition.y * touchSensitivity * 2.5f * Time.deltaTime;

                    player.transform.Rotate(Vector3.up * touchX);
                    xRotation -= touchY;
                    xRotation = Mathf.Clamp(xRotation, -90f, 90f);
                    Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                    break;
                }
            }
        }
    }
}
     
