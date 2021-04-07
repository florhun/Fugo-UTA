using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Animator _animator;

    public bool isGrounded;
    public bool isFlying;

    const string FLY = "FLY_NEW";
    const string IDLE = "main";
    const string FALL = "DOWN_NEW";
    public static int dir = 1;


    private void Update()
    {
        #region UNITY EDITOR

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            dir = 1;
            MovePlayer(dir);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            dir = -1;
            MovePlayer(dir);
        }


        #endregion

        CheckGround();
        
    }
    private float StartPos;

    public void MovePlayer(float newDir)
    {

        transform.DOKill();

        if(isFlying)
        {
            //Can't fly
            if (newDir == 1)
            {
                return;
            }
            //Turn Around
            else
            {
                _animator.Play(FALL);
                transform.DOMoveY(StartPos, 1.5f, false).
                    SetEase(Ease.OutBounce).
                    OnComplete(() =>
                    {
                        if (isGrounded)
                        {
                            _animator.Play(IDLE);
                        }
                    });
            }
        }
        else 
        {
            StartPos = transform.position.y;
            //FALL
            if(newDir == -1)
            {
                _animator.Play(FALL);
                transform.DOMoveY(StartPos - 1.5f, 1.5f, false).
                    SetEase(Ease.OutBounce).
                    OnComplete(() =>
                    {
                        _animator.Play(IDLE);
                    });
            }
            //FLY
            else
            {
                _animator.Play(FLY);
                transform.DOMoveY(StartPos + 1.5f, 1.5f, false).
                     SetEase(Ease.InOutBounce).
                     OnComplete(() =>
                     {
                         _animator.Play(IDLE);
                     });
            }
        }


    }


    private GameObject platform;
    public void CheckGround()
    {
        if (platform != null)
        {
            transform.position = new Vector3(platform.transform.position.x, transform.position.y,0);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            platform = collision.gameObject;
            isGrounded = true;

            print("collided" + collision.name) ;
        }
        if (collision.CompareTag("Corner"))
        {
            transform.position = new Vector3(transform.position.x, -6f, 0);
            transform.DOMoveY(-2.5f, 1.5f, false).
                SetEase(Ease.InOutBounce);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            //platform.transform.DetachChildren();
            platform = null;
            isGrounded = false;
        }
    }

}
