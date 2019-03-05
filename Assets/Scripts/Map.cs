/*
	Map.cs
	~~~~~~~~~~~~~~~~
	:license: MIT, see LICENSE for more details.
	:author: Gasquez "Fy-" Florian <florian@fy.to>
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {
	public Tile[] tiles;
	public Vector2Int size;

	public Map(Vector2Int size) {
		this.size = size;
		this.tiles = new Tile[this.size.x * this.size.y];

		// Map init
		for (int x = 0; x < this.size.x; x++) {
			for (int y = 0; y < this.size.y; y++) {
				this.tiles[x + y * this.size.x] = new Tile(x, y, TerrainType.Water, this);
			}
		}

		// Let's add some random stuff just to test this out. We will pretend our map is big enouth.
		// Obvisouly in a real game you should be able to load and save this, use procedural generation, etc...		this[2,2].terrainType = TerrainType.Dirt;
		this.SetRect(5, 5, 12, 12, TerrainType.Dirt);
		this.SetRect(5, 5, 5, 5, TerrainType.Grass);
		this.SetRect(9, 9, 9, 9, TerrainType.Rocks);

		Debug.Log("Map intialized with a size of" + this.size);
	}

	// We just set a rectangle to a terrainType value.
	public void SetRect(int startX, int startY, int width, int height, TerrainType terrainType) {
		for (int x = startX; x < startX+width; x++) {
			for (int y = startY; y < startY+height; y++) {
				this[x, y].terrainType = terrainType;
			}
		}
	}

	public IEnumerator<Tile> GetEnumerator() {
		for (int x = 0; x < this.size.x; x++) {
			for (int y = 0; y < this.size.y; y++) {
				yield return this[x, y];
			}
		}
	}

	public Tile this[Vector2Int v2] {
		get {
			return this[v2.x, v2.y];
		}
	}

	public Tile this[int x, int y] {
		get {
			if (x >= 0 && y >= 0 && x < this.size.x && y < this.size.y) {
				return this.tiles[x + y * this.size.x];
			}
			return null;
		} 
	}
}
