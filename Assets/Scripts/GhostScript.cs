using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    Vector3 moveDir;
    Transform cameraObject;

    InputManager inputManager;

    public float speed = 5f;
    public float rotationSpeed = 15f;
    
    private Rigidbody ghostRb;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        ghostRb = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }
    
    public void HandleMovement()
    {
        moveDir = cameraObject.forward * inputManager.forward;
        moveDir = moveDir + cameraObject.right * inputManager.horizontal;
        moveDir.Normalize();
        moveDir.y = 0;
        moveDir = moveDir * speed;

        Vector3 movementVelovity = moveDir;
        ghostRb.velocity = movementVelovity;
    }

    public void HandleRotation()
    {
        Vector3 targetDir = Vector3.zero;
        targetDir = cameraObject.forward * inputManager.forward;
        targetDir = targetDir + cameraObject.right * inputManager.horizontal;
        targetDir.Normalize();
        targetDir.y = 0;

        if(targetDir == Vector3.zero)
        {
            targetDir = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }
}
