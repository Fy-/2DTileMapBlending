/*
	Tile.cs
	~~~~~~~~~~~~~~~~
	:license: MIT, see LICENSE for more details.
	:author: Gasquez "Fy-" Florian <florian@fy.to>
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TerrainType {
	Water, Dirt, Grass, Rocks
}

public class Tile 
{	
	public TerrainType terrainType;
	public Vector2Int position;
	private Map _map;

	public Tile(Vector2Int position, TerrainType terrainType, Map map) {
		this.position = position;
		this.terrainType = terrainType;
		this._map = map;
	}

	public Tile(int x, int y, TerrainType terrainType, Map map) 
	: this(new Vector2Int(x, y), terrainType, map) {}
}