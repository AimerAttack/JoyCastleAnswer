using System;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    public Vector3 begin = Vector3.zero;
    public Vector3 end = Vector3.one * 5;
    public float duration = 2;
    public bool pingpang = false;
    public EaseType type = EaseType.Linear;

    private float _delta;
    private Vector3 _begin;
    private Vector3 _end;
    private float _duration;
    private bool _pingpang = false;
    private EaseType _type = EaseType.Linear;
    private bool _playing = false;
    private Transform _transform;

    public enum EaseType
    {
        Linear,
        EaseIn,
        EaseOut,
        EaseInOut,
    }
    
    void Start()
    {
        Move(gameObject,begin,end,duration,pingpang,type);
    }
    
    public void Move(GameObject gameObject, Vector3 begin, Vector3 end, float time, bool pingpong,EaseType type)
    {
        _delta = 0;
        _begin = begin;
        _end = end;
        _duration = time;
        _pingpang = pingpang;
        _type = type;
        _transform = gameObject.transform;

        _transform.position = begin;
        _playing = true;
    }

    private void Update()
    {
        if (!_playing)
            return;
        _delta += Time.deltaTime;
        
        if (UpdateMove() == 1)
        {
            FinishMove();
        }
    }

    float UpdateMove()
    {
        float percent = 0; 

        if (_type == EaseType.Linear)
        {
            percent = Mathf.Clamp01(_delta / duration);
            _transform.position = Vector3.Lerp(_begin, _end, percent);
        }
        else if (_type == EaseType.EaseIn)
        {
            percent = Mathf.Clamp01(Mathf.Pow(_delta / duration, 2));
            _transform.position = Vector3.Lerp(_begin, _end, percent);
        }
        else if (_type == EaseType.EaseOut)
        {
            percent = Mathf.Clamp01(Mathf.Pow(_delta / duration, 0.5f));
            _transform.position = Vector3.Lerp(_begin, _end, percent); 
        }
        else if (_type == EaseType.EaseInOut)
        {
            float cur = Mathf.Clamp01(_delta / duration);
            if (cur < 0.5f)
                percent = 2 * cur * cur;
            else
            {
                percent = 1 - 2 * (1 - cur) * (1 - cur);
            }

            percent = Mathf.Clamp01(percent);
            _transform.position = Vector3.Lerp(_begin, _end, percent); 
        }

        return percent;
    }

    void FinishMove()
    {
        if (_pingpang)
        {
            var tmp = _begin;
            _begin = _end;
            _end = tmp;
            Move(gameObject,_begin,_end,_duration,_pingpang,_type);
        }
        else
        {
            _playing = false;
        } 
    }
}