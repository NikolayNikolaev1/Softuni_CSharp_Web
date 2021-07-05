namespace CameraBazaar.Data.Enums
{
    using System;

    [Flags]
    public enum LightMeteringType
    {
        Spot = 1,
        CenterWeighted = 2,
        Evaluative = 4
    }
}
