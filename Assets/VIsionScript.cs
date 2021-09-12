using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIsionScript : MonoBehaviour
{
    public GameObject anna;
    public GameObject centerEye;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        anna.SetActive(centerEye.transform.position.x < 0.2 && centerEye.transform.position.x > -0.2 && centerEye.transform.position.y < 0.1 && centerEye.transform.position.y > -0.1) ;
    }
}
