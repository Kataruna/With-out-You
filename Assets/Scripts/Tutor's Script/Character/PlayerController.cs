using UnityEngine;
using UnityEngine.InputSystem;

 public class PlayerController : MonoBehaviour
{
    #region - Variable Declaration -
    
    [Header("Object Assign")]
    [SerializeField] private Transform mainCamera;
    
    [Header("Player Attribute")]
    [SerializeField] private float movementSpeed = 20f;
    [SerializeField] private bool moveAccordingToCamera;

    //Backdoor Variable
    private Vector2 _movementValue;

    #endregion

    #region - Unity's Method -

    private void Update()
    {
        MovementSystem();
    }
    
    //Unity's Input System Method
    public void OnMove(InputValue value) 
    {
        _movementValue = value.Get<Vector2>();
    }

    #endregion
    

    #region - Custom Method -

    private void MovementSystem()
    {
        if (moveAccordingToCamera)
        {
            Vector3 camForward = mainCamera.forward;
            Vector3 camRight = mainCamera.right;

            camForward.y = 0;
            camRight.y = 0;
            camForward = camForward.normalized;
            camRight = camRight.normalized;
            
            Move(camForward * _movementValue.y + camRight * _movementValue.x);
        }
        else
        {
            Move(new Vector3(_movementValue.x, 0, _movementValue.y));
        }
    }

    private void Move(Vector3 position) //
    {
        transform.position +=
            position * (movementSpeed * Time.deltaTime);
    }

    #endregion
}
