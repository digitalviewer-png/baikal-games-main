using System;
using UnityEngine;

namespace DoodleJump
{
    public interface IZoneObject
    {
        event Action ZoneLeft;
        void SetZone(Zone zone);
    }
}