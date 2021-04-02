using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    [CreateAssetMenu(menuName="Platform/Movement")]
    public class MovementSettings : ScriptableObject
    {
        [SerializeField] private float _speed;

        public float Speed { get { return _speed; } set { _speed = value; } }
    }
}
