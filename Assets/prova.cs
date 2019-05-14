using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prova : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        GameObject target = GameObject.FindGameObjectWithTag("CameraVR");
        Vector3 posizioneNuova = new Vector3((float)(target.transform.position.x + 0.22), (float)(target.transform.position.y - 0.45), (float)(target.transform.position.z - 0.48));
        transform.position = posizioneNuova;
    }
}
