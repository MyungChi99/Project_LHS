using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
    private float _speed;
    private bool _restoreTime;
    // Start is called before the first frame update
    private void Start()
    {
        _restoreTime = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if(_restoreTime)
        {
            if(Time.timeScale < 1f )
            {
                Time.timeScale += Time.deltaTime * _speed;
            }
            else
            {
                Time.timeScale = 1f;
                _restoreTime = false;
            }
        }
    }

    public void StopTime(float ChangeTime, int RestoreSpeed, float Delay)
    {
        _speed = RestoreSpeed;

        if(Delay>0)
        {
            StopCoroutine(StartTimeAgain(Delay));
            StartCoroutine(StartTimeAgain(Delay));
            
        }
        else
        {
            _restoreTime = true;
        }

        Time.timeScale = ChangeTime;
    }

    IEnumerator StartTimeAgain(float Amount)
    {
        _restoreTime = true;
        yield return new WaitForSecondsRealtime(Amount);
    }
}
