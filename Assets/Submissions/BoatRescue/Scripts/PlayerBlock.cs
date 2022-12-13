using System;
using System.Linq;
using UnityEngine;

namespace Submissions.BoatRescue
{
    public class PlayerBlock : LevelBlock
    {
        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private float _distanceTolerance = 0.001f;

        [Header("Position Gizmo")] [SerializeField]
        private bool _drawGizmo = true;

        [SerializeField] private float _gizmoSize = 0.1f;

        public override BlockType Type => BlockType.Player;

        public bool IsMoving { get; private set; }

        private Vector3 _requestedPosition;

        private void FixedUpdate()
        {
            if (!IsMoving)
            {
                return;
            }

            var targetPosition =
                Vector3.MoveTowards(transform.position, _requestedPosition, Time.deltaTime * _moveSpeed);
            transform.position = new Vector3(targetPosition.x, 0, targetPosition.z);

            var distanceRemaining = Vector3.Distance(transform.position, _requestedPosition);

            if (distanceRemaining < _distanceTolerance)
            {
                transform.position = _requestedPosition;
                IsMoving = false;
            }

            SetColumnRow();
        }

        public void Move(PlayerMoveDirection direction)
        {
            if (IsMoving)
            {
                return;
            }

            _requestedPosition = new Vector3(Column, 0, Row);
            var activeLevel = GameManager.Instance.ActiveLevel;

            switch (direction)
            {
                case PlayerMoveDirection.Up:
                    var rBlock = activeLevel.GetColumn(Column)
                        .FirstOrDefault(b => b.Row > Row && b.Type == BlockType.Barrier);
                    if (rBlock == null)
                    {
                        Debug.LogWarning("No Block found");
                        break;
                    }

                    _requestedPosition.z = rBlock.Row - 1;
                    break;
                case PlayerMoveDirection.Down:
                    rBlock = activeLevel.GetColumn(Column, true)
                        .FirstOrDefault(b => b.Row < Row && b.Type == BlockType.Barrier);
                    if (rBlock == null)
                    {
                        Debug.LogWarning("No Block found");
                        break;
                    }

                    _requestedPosition.z = rBlock.Row + 1;
                    break;
                case PlayerMoveDirection.Left:
                    var cBlock = activeLevel.GetRow(Row, true)
                        .FirstOrDefault(b => b.Column < Column && b.Type == BlockType.Barrier);
                    if (cBlock == null)
                    {
                        Debug.LogWarning("No Block found");
                        break;
                    }

                    _requestedPosition.x = cBlock.Column + 1;
                    break;
                case PlayerMoveDirection.Right:
                    cBlock = activeLevel.GetRow(Row)
                        .FirstOrDefault(b => b.Column > Column && b.Type == BlockType.Barrier);
                    if (cBlock == null)
                    {
                        Debug.LogWarning("No Block found");
                        break;
                    }

                    _requestedPosition.x = cBlock.Column - 1;
                    break;
            }

            IsMoving = true;
        }

        private void SetColumnRow()
        {
            Column = (int)transform.position.x;
            Row = (int)transform.position.z;
        }

        private void OnDrawGizmos()
        {
            if (!_drawGizmo)
            {
                return;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_requestedPosition, _gizmoSize);
            Gizmos.DrawLine(transform.position, _requestedPosition);
        }
    }
}