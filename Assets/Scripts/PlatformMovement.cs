using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Platform
{
    public class PlatformMovement : MonoBehaviour
    {
        public MovementSettings mov;
        public float width;
        public Ease easeM;

        private void Start()
        {
            MovePlatform(width);
        }

        private void MovePlatform(float targetPosX)
        {
            transform.DOMoveX(targetPosX, mov.Speed, false).SetEase(easeM).OnComplete(() => {MovePlatform(-targetPosX); });
        }
    }
}
