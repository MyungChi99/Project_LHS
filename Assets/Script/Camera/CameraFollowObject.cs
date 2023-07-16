using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _playerTransform;

    [Header("Flip Rotation Stats")]
    [SerializeField] private float _flipYRotationTime = 0.5f;

    private Coroutine _turnCoroutine;
    private Move _player;
    private bool _isFacingRight;

    private void Awake() 
    {
        _player = _playerTransform.gameObject.GetComponent<Move>();
        _isFacingRight = _player.isFacingRight;
    }

    private void Update() 
    {
        //make the cameraFollowObject follow the player's position
        transform.position = _playerTransform.position;
    }

    public void CallTurn()
    {
        _turnCoroutine = StartCoroutine(FlipYLerp());
    }
    private IEnumerator FlipYLerp()
    {
        float StartRotation = transform.localEulerAngles.y;
        float endRotationAmount = DetermineEndRotation();
        float yRotation =180f;

        float elapsedTime = 0f;
        while(elapsedTime < _flipYRotationTime)
        {
            elapsedTime += Time.deltaTime;

            //lerp the y rotation
            yRotation = Mathf.Lerp(StartRotation,endRotationAmount,(elapsedTime/_flipYRotationTime));
            transform.rotation = (Quaternion.Euler(0f,yRotation,0f));

            yield return null;
        }
    }

    private float DetermineEndRotation()
    {
        _isFacingRight = !_isFacingRight;
        if(_isFacingRight)
        {
            return 0f;
        }
        else
        {
            return 180f;
        }
    }
}
