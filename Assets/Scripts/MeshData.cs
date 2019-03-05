/*
	MeshData.cs
	~~~~~~~~~~~~~~~~
	:license: MIT, see LICENSE for more details.
	:author: Gasquez "Fy-" Florian <florian@fy.to>
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshData {
	public List<Vector3> vertices;
	public List<int> triangles;
	public List<Color> colors;
	public Mesh mesh;

	// You can use multiple constructor, for example one to specify the size of each list (or just use arrays)
	// Because we already know our vertice size (4 vertices per tile, so 4*width*height) 
	// and the same goes for triangles/indices (6*width*height).
	public MeshData() {
		this.vertices = new List<Vector3>();
		this.triangles = new List<int>();
		this.colors = new List<Color>();
		this.mesh = new Mesh();
	}

	public void AddTriangle(int vi, int a, int b, int c) {
		this.triangles.Add(vi+a);
		this.triangles.Add(vi+b);
		this.triangles.Add(vi+c);
	}

	public void NewMesh() {
		UnityEngine.Object.Destroy(this.mesh);
		this.mesh = new Mesh();
	}

	public void Clear() {
		this.vertices.Clear();
		this.triangles.Clear();
		this.colors.Clear();
		this.NewMesh();
	}

	public void Build() {
		this.mesh.SetVertices(this.vertices);
		this.mesh.SetTriangles(this.triangles, 0);
		if (this.colors.Count > 0) {
			this.mesh.SetColors(this.colors);
		}
	}
}