/*
	MapRenderer.cs
	~~~~~~~~~~~~~~~~
	:license: MIT, see LICENSE for more details.
	:author: Gasquez "Fy-" Florian <florian@fy.to>
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRenderer : MonoBehaviour
{
	public Map map;
	public bool ready = false;
	public bool gizmos = true;

	public void Start() {
		Res.LoadMats();

		this.map = new Map(new Vector2Int(40,40));

		MapMesh mapMesh = new MapMesh(this.map);

		foreach (KeyValuePair<TerrainType, MeshData> kv in mapMesh.meshes) {
			MeshData meshData = kv.Value;
			TerrainType terrainType = kv.Key;

			GameObject go = new GameObject("Mesh "+terrainType.ToString());
			go.transform.SetParent(this.transform);
			go.transform.localPosition = new Vector3(0, 0, -(int)terrainType);

			MeshFilter mf = go.AddComponent<MeshFilter>();
			mf.mesh = meshData.mesh;

			MeshRenderer mr = go.AddComponent<MeshRenderer>();
			mr.material = Res.mats[terrainType.ToString()];
			
		}

		this.ready = true;
	}

	 void OnDrawGizmosSelected() {
	 	if (this.ready && this.gizmos) {
		 	foreach (Tile tile in this.map) {
		 		if (tile.terrainType == TerrainType.Water) {
		 			Gizmos.color = Color.blue;
		 		} else if (tile.terrainType == TerrainType.Dirt) {
		 			Gizmos.color = Color.yellow;
		 		} else if (tile.terrainType == TerrainType.Grass) {
		 			Gizmos.color = Color.green;
		 		} else {
		 			Gizmos.color = Color.grey;
		 		}
		 		// DrawCube(v3 center, v3 size) we position from the center, so we need to add .5f;
		 		Gizmos.DrawCube(new Vector3(tile.position.x+.5f, tile.position.y+.5f), Vector3.one);
		 	}
	 	}
	 }
}
