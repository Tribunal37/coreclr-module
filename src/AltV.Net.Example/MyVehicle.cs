using System;
using AltV.Net.Elements.Entities;

namespace AltV.Net.Example
{
    public class MyVehicle : Vehicle, IMyVehicle
    {
        public int MyData { get; set; }

        public MyVehicle(IntPtr nativePointer, ushort id) : base(nativePointer, id)
        {
            MyData = 6;
        }
    }
}