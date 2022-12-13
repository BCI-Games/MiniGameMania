using System;
using UnityEngine;

namespace Submissions.BoatRescue
{
    [CreateAssetMenu(fileName = "LevelBuilderInstructions", menuName = "Puzzles/BuilderInstructions", order = 0)]
    public class LevelBuilderInstructions : ScriptableObject
    {
        [Serializable]
        public class LevelBlockInstruction
        {
            public Vector2 Position;
            public BlockType Type;
        }
    
        public int LevelNumber = 1;
        public Vector2 LevelSize = Vector2.one;
        public Vector3 CameraPosition;
        public LevelBlockInstruction[] LevelBlocks;
    }
}