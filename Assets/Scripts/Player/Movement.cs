using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float Speed = 10.0f;
 
    void Start()
    {
        
    }

    void Update()
    {
        // maybe switch to get axis raw?
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Translate(horizontal * Speed * Time.deltaTime, vertical * Speed * Time.deltaTime, 0.0f);
    }
}
