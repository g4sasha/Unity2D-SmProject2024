using UnityEngine;

namespace New
{
	public class UnitRigidbodyMovable : UnitMovable
	{
		private Rigidbody2D _unitRigidbody;

        public UnitRigidbodyMovable(Rigidbody2D rigidbody)
        {
            _unitRigidbody = rigidbody;
        }

        public override void Move(Vector2 direction)
        {
            var currentPosition = _unitRigidbody.position;
            _unitRigidbody.MovePosition(currentPosition + direction);
        }
    }
}