using Fusion;
using UnityEngine;
namespace MultiplayerDev
{

    public struct NetworkInputData : INetworkInput
    {
        public const byte INTERACT = 0x01;
        public const byte HIT = 0x02;
        public const byte DASH = 0x04;

        public byte buttons;
        public Vector3 direction;
        public Quaternion toRotation;
    }
}