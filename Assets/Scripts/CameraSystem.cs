using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSystem : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private bool useEdgeScrolling = false;
    [SerializeField] private float fieldOfViewMax = 24;
    [SerializeField] private float fieldOfViewMin = 3;
    [SerializeField] private int positionRange = 15;
    private float targetFieldOfView = 30f;



    private void Update()
    {

        CameraRotation();
        CameraZoom();
    }

    private void CameraMovement()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)) inputDir.x = -1f;
        if (Input.GetKey(KeyCode.S)) inputDir.x = +1f;
        if (Input.GetKey(KeyCode.A)) inputDir.z = -1f;
        if (Input.GetKey(KeyCode.D)) inputDir.z = +1f;


        int edgeScrollSize = 20;

        if (Input.mousePosition.x < edgeScrollSize) inputDir.z = -1f;
        if (Input.mousePosition.y < edgeScrollSize) inputDir.x = +1f;
        if (Input.mousePosition.x > Screen.width - edgeScrollSize) inputDir.z = +1f;
        if (Input.mousePosition.y > Screen.height - edgeScrollSize) inputDir.x = -1f;

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

        float moveSpeed = 50f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;


        if (transform.position.x > positionRange)
            transform.position = new Vector3(positionRange, transform.position.y, transform.position.z);
        if (transform.position.x < -positionRange)
            transform.position = new Vector3(-positionRange, transform.position.y, transform.position.z);

        if (transform.position.z > positionRange)
            transform.position = new Vector3(transform.position.x, transform.position.y, positionRange);
        if (transform.position.z < -positionRange)
            transform.position = new Vector3(transform.position.x, transform.position.y, -positionRange);

    }

    private void CameraRotation()
    {
        float rotateDir = 0f;


        int changeScrollSize = 20;

        if (Input.GetKey(KeyCode.Q)) rotateDir = +1f;
        if (Input.GetKey(KeyCode.E)) rotateDir = -1f;


        if (Input.mousePosition.x < changeScrollSize) rotateDir = +1;

        if (Input.mousePosition.x > Screen.width - changeScrollSize) rotateDir = -1f;


        float rotateSpeed = 75f;
        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0);
    }



    private void CameraZoom()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            targetFieldOfView -= 1f;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
           targetFieldOfView += 1f;
        }

        targetFieldOfView = Mathf.Clamp(targetFieldOfView, fieldOfViewMin, fieldOfViewMax);


        cinemachineVirtualCamera.m_Lens.FieldOfView = targetFieldOfView;


    }

}

   

   
