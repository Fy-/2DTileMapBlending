/*
	MapMesh.cs
	~~~~~~~~~~~~~~~~
	:license: MIT, see LICENSE for more details.
	:author: Gasquez "Fy-" Florian <florian@fy.to>
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapMesh 
{
	public Map map;
	public Dictionary<TerrainType, MeshData> meshes;

	public MapMesh(Map map) {
		this.map = map;
		this.meshes = new Dictionary<TerrainType, MeshData>();
		this.GenerateMesh();
	}

	public void GenerateMesh() {
		List<TerrainType> neighboursTerrainList = new List<TerrainType>();
		TerrainType[] neighboursTerrain = new TerrainType[8];
		Color[] _cols = new Color[9];

		foreach (Tile tile in this.map) {
			neighboursTerrainList.Clear();

			MeshData meshData = this.GetMesh(tile.terrainType); // Get or create a new mesh
			// Get the verticeIndex, it's just the vertice count in our MeshData object.

			int verticeIndex = meshData.vertices.Count; 
			meshData.vertices.Add(new Vector3(tile.position.x, tile.position.y)); // Vertice 0 (check image)
			meshData.vertices.Add(new Vector3(tile.position.x, tile.position.y+1)); // Vertice 1
			meshData.vertices.Add(new Vector3(tile.position.x+1, tile.position.y+1)); // Vertice 2
			meshData.vertices.Add(new Vector3(tile.position.x+1, tile.position.y)); // Vertice 3

			meshData.colors.Add(Color.white);
			meshData.colors.Add(Color.white);
			meshData.colors.Add(Color.white);
			meshData.colors.Add(Color.white);

			/*
			That's the first triangle (or TriangleA if you check the image).
			But we can just use our AddTriangle method in ou MeshData object.
			meshData.triangles.Add(verticeIndex);
			meshData.triangles.Add(verticeIndex+1);
			meshData.triangles.Add(verticeIndex+2);
			*/
			meshData.AddTriangle(verticeIndex, 0, 1, 2); // TriangleA
			meshData.AddTriangle(verticeIndex, 0, 2, 3); // TriangleB

			for (int i = 0; i < 8; i++) { // We have a max of 8 neighbours.
				// Let's get the neighbours with our Direction utility.
				Tile neighbour = this.map[tile.position+((Direction)i).Position()];

				if (neighbour != null) { 
					// If the neighbour is not null, we will set neighboursTerrain to the neighbour terrain type.
					neighboursTerrain[i] = neighbour.terrainType;
					if (
						neighbour.terrainType != tile.terrainType && // We add only if its different than current tile.
						!neighboursTerrainList.Contains(neighbour.terrainType) && // And if it's not in the list.
						(int)neighbour.terrainType >= (int)tile.terrainType // And we only blend when we're on top.
					) {
						neighboursTerrainList.Add(neighbour.terrainType);
					}
				} else {
					// If its null we will pretend it's the same as the current tile (we don't need to blend).
					neighboursTerrain[i] = tile.terrainType;
				}
			}

			foreach (TerrainType terrainType in neighboursTerrainList) {
				meshData = this.GetMesh(terrainType); // Get the new meshData (rocks for this example)
				verticeIndex = meshData.vertices.Count; // Get the meshData verticeIndx

				meshData.vertices.Add(new Vector3(tile.position.x+.5f, tile.position.y)); // 0
				meshData.vertices.Add(new Vector3(tile.position.x, tile.position.y)); // 1
				meshData.vertices.Add(new Vector3(tile.position.x, tile.position.y+.5f)); // 2
				meshData.vertices.Add(new Vector3(tile.position.x, tile.position.y+1)); // 3
				meshData.vertices.Add(new Vector3(tile.position.x+.5f, tile.position.y+1)); // 4
				meshData.vertices.Add(new Vector3(tile.position.x+1, tile.position.y+1)); // 5
				meshData.vertices.Add(new Vector3(tile.position.x+1, tile.position.y+.5f)); // 6
				meshData.vertices.Add(new Vector3(tile.position.x+1, tile.position.y)); // 7
				meshData.vertices.Add(new Vector3(tile.position.x+.5f, tile.position.y+.5f)); //8

				for (int i = 0; i < _cols.Length; i++) {
					_cols[i] = Color.clear;
				}
				for (int i = 0; i < 8; i++) {
					if (i % 2 != 0) { // If it's odd.
						if (terrainType == neighboursTerrain[i]) {
							_cols[i] = Color.white; // Odd indices trick
						}
					} else {
						if (terrainType == neighboursTerrain[i]) {
							switch (i) {
								case 0: // South
									_cols[1] = Color.white;
									_cols[0] = Color.white;
									_cols[7] = Color.white;
									break;
								case 2:  // West
									_cols[1] = Color.white;
									_cols[2] = Color.white;
									_cols[3] = Color.white;
									break;
								case 4: // North
									_cols[3] = Color.white;
									_cols[4] = Color.white;
									_cols[5] = Color.white;
									break;
								case 6: // East
									_cols[5] = Color.white;
									_cols[6] = Color.white;
									_cols[7] = Color.white;
									break;
							}
						}
					}
				}

				meshData.colors.AddRange(_cols);
				meshData.AddTriangle(verticeIndex, 0, 8, 6);
				meshData.AddTriangle(verticeIndex, 0, 6, 7);
				meshData.AddTriangle(verticeIndex, 1, 8, 0);
				meshData.AddTriangle(verticeIndex, 1, 2, 8);
				meshData.AddTriangle(verticeIndex, 2, 4, 8);
				meshData.AddTriangle(verticeIndex, 2, 3, 4);
				meshData.AddTriangle(verticeIndex, 8, 5, 6);
				meshData.AddTriangle(verticeIndex, 8, 4, 5);
			}
		}

		foreach (MeshData meshData in this.meshes.Values) {
			meshData.Build();
		}
	}

	public MeshData GetMesh(TerrainType terrainType) {
		if (this.meshes.ContainsKey(terrainType)) {  // We already know this terrain type, let's return the mesh
			return this.meshes[terrainType];
		}
		this.meshes.Add(terrainType, new MeshData()); // It's a new terrain type, let's create a new MeshData 
		return this.meshes[terrainType];
	}
}
