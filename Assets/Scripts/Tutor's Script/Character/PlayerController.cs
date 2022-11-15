using UnityEngine;
using UnityEngine.InputSystem;

 public class PlayerController : Singleton<PlayerController>
{
    #region - Variable Declaration -

    [Header("Object Assign")]
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private Animator flipAnimator;
    
    [Header("Player Attribute")]
    [SerializeField] private float movementSpeed = 20f;
    [SerializeField] private float sprintSpeedMultiply = 2f;
    [SerializeField] private bool moveAccordingToCamera;

    private bool _isControlable = true;

    //Backdoor Variable
    private Vector2 _movementValue;
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int Left = Animator.StringToHash("Left");

    #endregion

    #region - Unity's Method -

    private void Update()
    {
        MovementSystem();
    }
    
    //Unity's Input System Method
    public void OnMove(InputValue value) 
    {
        if(_isControlable) _movementValue = value.Get<Vector2>();
        else _movementValue = Vector2.zero;

        if(_movementValue.x != 0f || _movementValue.y != 0f) characterAnimator.SetBool(IsMoving, true);
        else characterAnimator.SetBool(IsMoving, false);
        
        if(_movementValue.x == 0f) return;

        if(_movementValue.x < 0) flipAnimator.SetBool(Left, true);
        else flipAnimator.SetBool(Left, false);
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
        if(Input.GetKeyDown(KeyCode.LeftShift)) transform.position += position * (movementSpeed * sprintSpeedMultiply * Time.deltaTime);
        else
        {
            transform.position +=
                position * (movementSpeed * Time.deltaTime);
        }
    }

    public void SetControlState(bool value)
    {
        _isControlable = value;
    }

    #endregion
}
