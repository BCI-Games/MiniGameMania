using UnityEngine;

namespace Submissions.BoatRescue
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private Vector2 _gridSize;

        [Header("Level Blocks")] [SerializeField]
        private LevelBlock _playerPrefab;

        [SerializeField] private LevelBlock _rewardPrefab;
        [SerializeField] private LevelBlock _punishmentPrefab;
        [SerializeField] private LevelBlock _clearAreaPrefab;
        [SerializeField] private LevelBlock _barrierPrefab;

        private void Awake()
        {
            GameManager.Instance.RegisterBuilder(this);
        }

        private void OnDestroy()
        {
            if (GameManager.Instance == null) return;
            GameManager.Instance.UnregisterBuilder(this);
        }

        public Level BuildLevel(LevelBuilderInstructions buildInstructions)
        {
            var builtLevel = new Level
            {
                LevelNumber = buildInstructions.LevelNumber,
                LevelSize = buildInstructions.LevelSize,
            };

            //Fit Camera
            var cameraT = Camera.main.transform;
            cameraT.position = buildInstructions.CameraPosition;

            BuildPuzzleBorder(ref builtLevel);

            //Build blocks from instructions
            foreach (var blockInstruction in buildInstructions.LevelBlocks)
            {
                BuildBlock(blockInstruction, ref builtLevel);
            }

            return builtLevel;
        }

        private void BuildPuzzleBorder(ref Level builtLevel)
        {
            //Left Columns
            for (int i = -1; i < builtLevel.LevelSize.y + 2; i++)
            {
                BuildBlock(new LevelBuilderInstructions.LevelBlockInstruction
                {
                    Position = new Vector2(-1, i),
                    Type = BlockType.Barrier
                }, ref builtLevel);
            }

            //Right Columns
            for (int i = -1; i < builtLevel.LevelSize.y + 2; i++)
            {
                BuildBlock(new LevelBuilderInstructions.LevelBlockInstruction
                {
                    Position = new Vector2(builtLevel.LevelSize.x + 1, i),
                    Type = BlockType.Barrier
                }, ref builtLevel);
            }

            //Top Row
            for (int i = 0; i < builtLevel.LevelSize.x + 1; i++)
            {
                BuildBlock(new LevelBuilderInstructions.LevelBlockInstruction
                {
                    Position = new Vector2(i, builtLevel.LevelSize.y + 1),
                    Type = BlockType.Barrier
                }, ref builtLevel);
            }

            //Bottom Row
            for (int i = 0; i < builtLevel.LevelSize.x + 1; i++)
            {
                BuildBlock(new LevelBuilderInstructions.LevelBlockInstruction
                {
                    Position = new Vector2(i, -1),
                    Type = BlockType.Barrier
                }, ref builtLevel);
            }
        }

        private void BuildBlock(LevelBuilderInstructions.LevelBlockInstruction blockInstruction, ref Level level)
        {
            var levelBlock = InstantiateBlockType(blockInstruction.Type);
            levelBlock.Column = (int)blockInstruction.Position.x + 1;
            levelBlock.Row = (int)blockInstruction.Position.y + 1;

            levelBlock.transform.position =
                new Vector3(levelBlock.Column * _gridSize.x, 0, levelBlock.Row * _gridSize.y);

            level.AddBlock(levelBlock);
        }

        public void DestroyLevel(Level level)
        {
            if (level == null)
            {
                return;
            }

            foreach (var levelBlock in level.LevelBlocks)
            {
                Destroy(levelBlock.gameObject);
            }

            foreach (var player in level.Players)
            {
                Destroy(player.gameObject);
            }

            level.ResetLevelData();
        }

        private LevelBlock InstantiateBlockType(BlockType blockType)
        {
            GameObject blockGO;
            switch (blockType)
            {
                case BlockType.Player:
                    blockGO = Instantiate(_playerPrefab.gameObject, transform);
                    break;
                case BlockType.Barrier:
                    blockGO = Instantiate(_barrierPrefab.gameObject, transform);
                    break;
                case BlockType.Reward:
                    blockGO = Instantiate(_rewardPrefab.gameObject, transform);
                    break;
                case BlockType.Punishment:
                    blockGO = Instantiate(_punishmentPrefab.gameObject, transform);
                    break;
                case BlockType.ClearArea:
                    blockGO = Instantiate(_clearAreaPrefab.gameObject, transform);
                    break;
                default:
                    return null;
            }

            return blockGO != null ? blockGO.GetComponent<LevelBlock>() : null;
        }
    }
}