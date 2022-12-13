using System;

namespace Submissions.BoatRescue
{
    [Serializable]

    public enum BlockType
    {
        Player,
        Barrier,
        Reward,
        Punishment,
        ClearArea,
    }
}