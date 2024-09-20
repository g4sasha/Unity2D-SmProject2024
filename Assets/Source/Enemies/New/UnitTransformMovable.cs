using UnityEngine;

namespace New
{
	public class UnitTransformMovable : UnitMovable
	{
		private Transform _unitTransform;

        public override void Move(Vector2 direction)
        {
            _unitTransform.Translate(direction);
        }
    }
}