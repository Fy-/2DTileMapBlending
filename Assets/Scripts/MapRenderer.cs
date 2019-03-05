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
			MeshData meshData = kv.Value; // It's just easier to read, you don't need to do this.
			TerrainType terrainType = kv.Key; // It's just easier to read, you don't need to do this.
			GameObject go = new GameObject("Mesh for "+terrainType.ToString());
			go.transform.SetParent(this.transform);

			// In our TerrainType enum Water=0, Dirt=1, Grass=2, Rocks=3
			// We always want to draw Rocks over Grass, Grass over Dirt, Dirt over Water
			// So we can just use the negative integer value as the Z position for the GameObject.
			go.transform.localPosition = new Vector3(0, 0, -(int)terrainType); 

			// Add a mesh filter and set the mesh to our mesh.
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
