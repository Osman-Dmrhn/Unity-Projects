using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class CompassScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player; 
    public Transform kuzey;
    public RawImage pusula;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDir = kuzey.position - player.position;
        float aci = Vector3.SignedAngle(player.forward, targetDir, Vector3.up);
        pusula.rectTransform.localEulerAngles = new Vector3(0, 0, -aci);
    }
}
