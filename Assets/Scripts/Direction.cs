/*
	Direction.cs
	~~~~~~~~~~~~~~~~
	:license: MIT, see LICENSE for more details.
	:author: Gasquez "Fy-" Florian <florian@fy.to>
*/

using UnityEngine;

public enum Direction : ushort {
	S, SW, W, NW, N, NE, E, SE
}

public static class DirectionExtensions {
	public static Vector2Int Position(this Direction direction) {
		switch (direction) {
			case Direction.S:
				return new Vector2Int(0, -1);
			case Direction.SW:
				return new Vector2Int(-1, -1);
			case Direction.W:
				return new Vector2Int(-1, 0);
			case Direction.NW:
				return new Vector2Int(-1, 1);
			case Direction.N:
				return new Vector2Int(0, 1);
			case Direction.NE:
				return new Vector2Int(1, 1);
			case Direction.E:
				return new Vector2Int(1, 0);
			case Direction.SE:
				return new Vector2Int(1, -1);
			default:
				return Vector2Int.zero;
		}
	}
}