using System;
using UnityEngine;

namespace Submissions.BoatRescue
{
    [Serializable]

    public class LevelBlock : MonoBehaviour
    {
        [SerializeField] private BlockType _blockType;

        public virtual BlockType Type => _blockType;

        public int Row;
        public int Column;

        private void OnDestroy()
        {
            if (GameManager.Instance != null && GameManager.Instance.ActiveLevel != null)
            {
                GameManager.Instance.ActiveLevel.RemoveBlock(this);
            }
        }
    }
}