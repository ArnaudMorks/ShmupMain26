using UnityEngine;

public class SC_ModelMovementPlayer : MonoBehaviour
{
    [SerializeField] private float _maxAngle;
    [SerializeField] private float _tiltSpeed;
    [SerializeField] private AnimationCurve _tiltCurve;
    private float _tiltIndex;

    private void Update()
    {
        switch(GetMovementHorizontal())
        {
            case -1:
                _tiltIndex += _tiltSpeed * Time.deltaTime;
                break;
            case 1:
                _tiltIndex -= _tiltSpeed * Time.deltaTime;
                break;
            default:
                if(_tiltIndex > 0)
                {
                    _tiltIndex -= _tiltSpeed * Time.deltaTime;

                    if(_tiltIndex < 0)
                    {
                        _tiltIndex = 0;
                    }
                }
                else if(_tiltIndex < 0)
                {
                    _tiltIndex += _tiltSpeed * Time.deltaTime;

                    if(_tiltIndex > 0)
                    {
                        _tiltIndex = 0;
                    }
                }

                break;
        }

        _tiltIndex = Mathf.Clamp(_tiltIndex, -1, 1);
        transform.rotation = Quaternion.Euler(0, 0, _tiltCurve.Evaluate(_tiltIndex) * _maxAngle);
    }

    private float GetMovementHorizontal() => Input.GetAxisRaw("Horizontal");
}
