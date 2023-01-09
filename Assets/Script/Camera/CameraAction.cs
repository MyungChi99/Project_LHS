using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    In UnrealEngine 
    Xaixs = Z
    Yaxis = X
    Zaxis = Y
*/
public class Camera : MonoBehaviour
{

    //카메라 위치 반환하는 법 알면 지워야함
    private CameraLocationReturn _camera;
    [SerializeField] private GameObject Follow;
    [SerializeField] private float _distanceFromCharacter;

    private float _rightLimit, _leftLimit, _upLimit, _downLimit;
    private float _aspecio = 0.5625f;
    public float idlecamera_yaixs_bias;
    public float idlecamera_xaixs_bias;
    public float idlecamera_zaixs_bias;
    private Vector3 _playerLocaiton;
    private bool _isWallJumping;
    // 아직 구현안한 기능이지만 구현할예정(메트로베니아 게임 필수임 ㅋ)

    private Rigidbody2D _body;
    private Vector2 _velocity;
    private float _yaxis_LerpDegree, _xaxis_LerpDegree;

    private void Start()
    {
        _playerLocaiton = Follow.transform.position;
        _body = Follow.GetComponent<Rigidbody2D>();
        _velocity = _body.velocity;
    }

    private void FixedUpdate() 
    {
        
    }
    private void DetachCamera()
    {
        //Detach camera from lerping;
    }

    //필요없을거같음
    // private void SetWorldLocation(float x, float y, float z)
    // {
    //     Vector3 Camera_Location =new Vector3(x,y,z);
    //     transform.position = Camera_Location;
    // } 
    private float VerticalMovement()
    {
        //high Lerp (Instance following camera)
        if(_velocity.y <-1.0f && _camera.ReturnCameraPosition().y-TargetWithoutClamp().y > 0 && _camera.ReturnCameraPosition().y-TargetLocation().y >0)
        {
            _yaxis_LerpDegree = 25.0f;
        }
        else
        {
            _yaxis_LerpDegree = 3.5f;
        }
        return (Mathf.Lerp(_camera.ReturnCameraPosition().y,TargetLocation().y,Time.deltaTime*_yaxis_LerpDegree));
    }  

    private float HorizontalMovement()
    {
        if((Mathf.Abs(_camera.ReturnCameraPosition().x-TargetLocation().y)) > _distanceFromCharacter/3 )
        {
            _xaxis_LerpDegree = Mathf.Abs(_distanceFromCharacter/(_camera.ReturnCameraPosition().x-TargetLocation().x));
        }
        else
        {
            if(Mathf.Abs(_velocity.x)<=0.5f && !_isWallJumping)
            {
                _xaxis_LerpDegree = 1.5f;
            }
            else if(Mathf.Abs(_velocity.x)>0.5f && !_isWallJumping)
            {
                _xaxis_LerpDegree = 4.0f;
            }
            else if(_isWallJumping)
            {
                _xaxis_LerpDegree = 1.0f;
            }
        }
        return (Mathf.Lerp(_camera.ReturnCameraPosition().x,TargetLocation().x,Time.deltaTime*_xaxis_LerpDegree));
    }

    private Vector3 TargetWithoutClamp()
    {
        int isfacingRight=1;
        if(_velocity.x>0)
            isfacingRight = 1;
        else if(_velocity.x<0)
            isfacingRight = -1;
        Vector3 camerabias = new Vector3(idlecamera_xaixs_bias,idlecamera_yaixs_bias*isfacingRight,idlecamera_zaixs_bias);

        return(_playerLocaiton + camerabias);
    }
    private Vector3 TargetLocation()
    {
        return new Vector3(ClampXAxis(),ClampYAxis(),TargetWithoutClamp().z);
    }

    private float ClampXAxis()
    {
        return (Mathf.Clamp(TargetWithoutClamp().x,_leftLimit+_distanceFromCharacter,_rightLimit+_distanceFromCharacter));
    }
    private float ClampYAxis()
    {
        //16대9 가정 변수값 0.5625 = 9/16
        return (Mathf.Clamp(TargetWithoutClamp().y,_downLimit+(_distanceFromCharacter*_aspecio),_upLimit-(_distanceFromCharacter*_aspecio)));
    }
}
