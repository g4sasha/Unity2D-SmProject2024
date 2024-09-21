using UnityEngine;

namespace New
{
	public class UnitInputListener
	{
		public bool Locked { get; set; }

		public Vector2 GetMoveAxes()
		{
			if (Locked)
			{
				return Vector2.zero;
			}

			return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		}
    }
}