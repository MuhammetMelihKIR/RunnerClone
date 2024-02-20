using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    // Player Movement Variables
    private float _jumpForce = 8f; 
    private float _downForce = 20f; 
    private float _switchLaneValue = 1f;
    private float _speed = 5;
   
    //BOOLS
    private bool _isGrounded;
    
    //COMPONENTS
    private PlayerAnimation _playerAnimation;
    private CapsuleCollider _collider;
    private Rigidbody _rb;
    private void OnEnable()
    {
        UIManager.OnGameReset += UIManager_OnGameStart;
    }
    private void OnDestroy()
    {
        UIManager.OnGameReset -= UIManager_OnGameStart;
    }
    private void UIManager_OnGameStart()
    {
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
        DefaultColliderSize();
        _speed = 5;
    }
    private void Awake() {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _playerAnimation = GetComponent<PlayerAnimation>();
    }
    
    private void Update() {
        InputKey();
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.IsGameState(GameState.InGame))
        {
            Vector3 movement = transform.forward * (_speed * Time.deltaTime);
            _rb.MovePosition(_rb.position + movement);
        }
    }
    private void InputKey() {
        
        if (_isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.W)) {
                Jump();
                _playerAnimation.Jump();
                DefaultColliderSize();
                
            }
            else if (Input.GetKeyDown(KeyCode.S)) {
                SlideColliderSize();
                _playerAnimation.Roll();
                SetIsGrounded();
            }
        }
        else 
        {
            if (Input.GetKeyDown(KeyCode.S)) {
                _rb.velocity += Vector3.down * (_downForce *2);
                DefaultColliderSize(); 
            }
        }
        if (Input.GetKeyDown(KeyCode.S) && !_isGrounded) {
            
            SlideColliderSize();
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            SwitchLane(-3);
            DefaultColliderSize(); 
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            SwitchLane(3);
            DefaultColliderSize(); 
        }
    }
    private void Jump() {
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _isGrounded = false;
        
        if (!_isGrounded)
        {
            _rb.velocity += Vector3.down * (_downForce * Time.deltaTime * 5f);
        }
    }
    private void SwitchLane(int direction) {
        Vector3 newPosition = transform.position + Vector3.right * (direction * _switchLaneValue);
        newPosition.x = Mathf.Clamp(newPosition.x, -3f, 3f);
        transform.position = newPosition;
    }
    private void DefaultColliderSize() {
        _collider.radius = 0.5f;
        _collider.height = 2f;
        _collider.center = new Vector3(0, 1, 0);
    }
    private void SlideColliderSize() {
        _collider.radius = 0.3f;
        _collider.height = 0.5f;
        _collider.center = new Vector3(0, 0.3f, 0);
    }
    public bool SetIsGrounded() {
        _isGrounded = true;
        return _isGrounded;
    }
   
}
