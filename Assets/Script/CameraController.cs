using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [Header("Move")] private float moveSpeed;
    [SerializeField] private float xInput;
    [SerializeField] private float zInput;

    public static CameraController instance;

    private void Awake()
    {
        instance = this;
        cam = Camera.main;
    }//Awake


    void Start()
    {
        moveSpeed = 50;
    }// Start 

    
    void Update()
    {
        MoveByKB();
    }// Update 

    void MoveByKB()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        Vector3 dir = (transform.forward * zInput) + (transform.right * xInput);
        transform.position += dir * moveSpeed * Time.deltaTime;

    }


}//CameraController
