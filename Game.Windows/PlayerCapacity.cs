using System;

namespace Game.Windows
{
    [Flags]
    public enum PlayerCapacity
    {
        Solo = 1 << 0,
        Duo = 1 << 1,
        Trio = 1 << 2,
        SoloDuo = Solo | Duo,
        SoloMulti = SoloDuo | Trio,
        Multi = Duo | Trio
    }
}