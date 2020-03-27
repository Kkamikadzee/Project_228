using UnityEngine;

public class Swipe : MonoBehaviour
{
    public uint DEADZONE;
    private bool _swipeLeft, _swipeRight, _swipeUp, _swipeDown;
    private bool _isDraging;
    private Vector2 _startTouch, _swipeDelta;

    private void Update()
    {
        _swipeLeft = _swipeRight = _swipeUp = _swipeDown = false;

        #region Standalone Inputs
        if(Input.GetMouseButtonDown(0))
        {
            _isDraging = true;
            _startTouch = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            Reset();
        }
        #endregion

        #region Mobile Inputs
        if(Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                _isDraging = true;
                _startTouch = Input.touches[0].position;
            }
            else if(Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                Reset();
            }
        }
        #endregion

        _swipeDelta = Vector2.zero;
        if(_isDraging)
        {
            if (Input.touches.Length > 0)
                _swipeDelta = Input.touches[0].position - _startTouch;
            else if (Input.GetMouseButton(0))
                _swipeDelta = (Vector2)Input.mousePosition - _startTouch;
            if (_swipeDelta.magnitude > DEADZONE)
            {
                float x = _swipeDelta.x;
                float y = _swipeDelta.y;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x < 0)
                        _swipeLeft = true;
                    else
                        _swipeRight = true;
                }
                else
                {
                    if (y < 0)
                        _swipeDown = true;
                    else
                        _swipeUp = true;
                }
            }
        }
    }

    public int OneSwipeDirection()
    {
        if (_swipeLeft)
        {
            Reset();
            return 1;
        }
        else if (_swipeRight)
        {
            Reset();
            return 2;
        }
        else if (_swipeUp)
        {
            Reset();
            return 3;
        }
        else if (_swipeDown)
        {
            Reset();
            return 4;
        }
        else
            return 0;
    }

    private void Reset()
    {
        _isDraging = false;
        _startTouch = _swipeDelta = Vector2.zero;
    }

    public Vector2 StartTouch { get { return _startTouch; } }
    public Vector2 SwipeDelta { get { return _swipeDelta; } }
    public bool SwipeLeft { get { return _swipeLeft; } }
    public bool SwipeRight { get { return _swipeRight; } }
    public bool SwipeUp { get { return _swipeUp; } }
    public bool SwipeDown { get { return _swipeDown; } }
    public bool IsDraging { get { return _isDraging; } }
}
