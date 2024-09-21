using UnityEngine;

namespace New
{
	public class UnitInputListener
	{
		public Vector2 GetMoveAxes()
		{
			return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		}
	}
}