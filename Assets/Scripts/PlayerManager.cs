using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    GhostScript playerScript;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        playerScript = GetComponent<GhostScript>();
    }

    private void Update()
    {
        inputManager.HandleMovementInput();
    }

    private void FixedUpdate()
    {
        playerScript.HandleMovement();
        playerScript.HandleRotation();
    }
}
