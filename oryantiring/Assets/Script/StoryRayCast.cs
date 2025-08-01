using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StoryRayCast : MonoBehaviour
{
    public GameObject controller;
    public TMP_Text raycasttext;
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

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 7))
                {
                    if (hit.collider.gameObject.tag == "flags")
                    {
                        raycasttext.text = hit.collider.gameObject.name;
                        StartCoroutine(ClearTextAfterDelay(3));

                        GameObject clickedObject = hit.collider.gameObject;

                        if (clickedObject.gameObject.tag == "flags")
                        {
                            controller.GetComponent<StoryGameController>().topla();
                            Destroy(clickedObject);
                        }
                    }
                    else if (hit.collider.gameObject.tag == "chest")
                    {
                        controller.GetComponent<StoryGameController>().kazanma();
                    }
                    else
                    {
                        StartCoroutine(ClearTextAfterDelay(3));
                    }
                }
                
            }
        }
    }
    IEnumerator ClearTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        raycasttext.text = "";
    }
}
