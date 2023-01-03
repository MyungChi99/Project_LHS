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
    
    private Rigidbody2D _body;
    private Vector2 _velocity;
    private float _yaxis_LerpDegree, _xaixs_LerpDegree;

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
    private void VerticalMovement()
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

    }  

    private void HorizontalMovement()
    {
 
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
