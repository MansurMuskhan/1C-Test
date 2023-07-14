using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : Unit
{
    [SerializeField]
    float
        _accelerationTime = .1f,
        _moveSpeed = 6;
    float
        _runSoundTime,
        _velocityXSmoothing,
        _velocityYSmoothing;
    Controller2D _controller;
    Vector2 _velocity;
    Vector2 _inputAxis => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    Vector3 _startPos;
    Vector2 _camPos;
    float _health;
    public override float Unit_Health
    {
        get { return _health; }
        protected set
        {
            _health = value;
            UILinks.PlayerHealthText.text = "Health: " + _health;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        ActionsService.RestartGame += RestartGame;
        ActionsService.PlayerGetDamage += Unit_Damage;
    }
    void Start()
    {
        _startPos = transform.position;
        Unit_Health = 10;
		_controller = GetComponent<Controller2D>();
        _camPos = Camera.main.transform.position;
    }
    

    void Update()
    {
        _velocity.x = Mathf.SmoothDamp(_velocity.x, _inputAxis.x * _moveSpeed, ref _velocityXSmoothing, _accelerationTime);
        _velocity.y = Mathf.SmoothDamp(_velocity.y, _inputAxis.y * _moveSpeed, ref _velocityYSmoothing, _accelerationTime);
        _controller.Move(_velocity * Time.deltaTime);
        Vector2 zone = MovementZone.Bounds;
        Vector2 vel;
        vel.x = Mathf.Clamp(transform.position.x, _camPos.x - zone.x, _camPos.x + zone.x);
        vel.y = Mathf.Clamp(transform.position.y, _camPos.y - zone.y, Settings.ZONE_LINE);
        transform.position = vel;

        if (_inputAxis.magnitude > 0)
        {
            if(_runSoundTime < 0)
            {
                SoundsService.PlayRange(AudioRangeName.playerRun, 0.2f);
                _runSoundTime = .3f;
            }
            _runSoundTime -= Time.deltaTime;
        }
    }

    public override void Unit_Init<T>(T t)
    {
        throw new System.NotImplementedException();
    }

    protected override void Die()
    {
        ActionsService.GameOver.Invoke();
    }
    void RestartGame()
    {
        transform.position = _startPos;
        Unit_Health = 10;
    }
}
