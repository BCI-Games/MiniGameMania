using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Submissions.BoatRescue
{
    [Serializable]

    public class Level
    {
        public int LevelNumber;
        public Vector2 LevelSize = Vector2.one;

        public readonly List<PlayerBlock> Players = new();
        public readonly List<LevelBlock> LevelBlocks = new();
        public readonly Dictionary<int, List<LevelBlock>> Rows = new();
        public readonly Dictionary<int, List<LevelBlock>> Columns = new();

        public int TotalRewards => LevelBlocks.Count(b => b.Type == BlockType.Reward);

        public void AddBlock(LevelBlock block)
        {
            if (block.Type == BlockType.Player && block is PlayerBlock player)
            {
                Players.Add(player);
                return;
            }

            LevelBlocks.Add(block);

            if (Columns.TryGetValue(block.Column, out var cBlocks))
            {
                cBlocks.Add(block);
            }
            else
            {
                Columns.Add(block.Column, new List<LevelBlock> { block });
            }

            if (Rows.TryGetValue(block.Row, out var rBlocks))
            {
                rBlocks.Add(block);
            }
            else
            {
                Rows.Add(block.Row, new List<LevelBlock> { block });
            }
        }

        public void RemoveBlock(LevelBlock block)
        {
            if (block.Type == BlockType.Player && block is PlayerBlock player)
            {
                Players.Remove(player);
                return;
            }

            LevelBlocks.Remove(block);

            if (Rows.TryGetValue(block.Row, out var blocks))
            {
                blocks.Remove(block);
            }

            if (Columns.TryGetValue(block.Column, out blocks))
            {
                blocks.Remove(block);
            }
        }

        public void ResetLevelData()
        {
            Players.Clear();
            LevelBlocks.Clear();
            Rows.Clear();
            Columns.Clear();
        }

        public IEnumerable<LevelBlock> GetRow(int row, bool reverse = false)
        {
            if (Rows.TryGetValue(row, out var blocks))
            {
                if (reverse)
                {
                    return blocks.OrderByDescending(b => b.Column);
                }

                return blocks.OrderBy(b => b.Column);
            }

            return ArraySegment<LevelBlock>.Empty;
        }

        public IEnumerable<LevelBlock> GetColumn(int column, bool reverse = false)
        {
            if (Columns.TryGetValue(column, out var blocks))
            {
                if (reverse)
                {
                    return blocks.OrderByDescending(b => b.Row);
                }

                return blocks.OrderBy(b => b.Row);
            }

            return ArraySegment<LevelBlock>.Empty;
        }

        public bool PlayersMoving()
        {
            var moving = Players.Count(p => p.IsMoving) > 0;
            return moving;
        }
    }
}