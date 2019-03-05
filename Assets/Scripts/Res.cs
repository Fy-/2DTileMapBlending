/*
	Res.cs
	~~~~~~~~~~~~~~~~
	:license: MIT, see LICENSE for more details.
	:author: Gasquez "Fy-" Florian <florian@fy.to>
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Res {
	public static Dictionary<string, Material> mats;

	public static void LoadMats() {
		Material[] mats = Resources.LoadAll<Material>("Materials/");
		Res.mats = new Dictionary<string, Material>();
		foreach (Material mat in mats) {
			Res.mats.Add(mat.name, mat);
		}
	}
}

