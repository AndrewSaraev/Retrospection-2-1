// #if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
 
class MipMapBiasMenu {

	static Object[] selection;
 
	[MenuItem("Assets/Set Global Mipmap Bias -0.75", true)]
	static bool ValidateSetBias075() {
		selection = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
		return (selection.Length > 0);
	}
	
	[MenuItem("Assets/Set Global Mipmap Bias -0.75", false, 1011)]
	static void SetBias075() {
		foreach (Texture texture in selection) {
			string path = AssetDatabase.GetAssetPath(texture);
			(AssetImporter.GetAtPath(path) as TextureImporter).mipMapBias = -0.75f;
			AssetDatabase.ImportAsset(path);
		}
	}
	
	[MenuItem("Assets/Set Global Mipmap Bias -0.50", true)]
	static bool ValidateSetBias05() {
    	selection = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
    	return (selection.Length > 0);
	}
	
	[MenuItem("Assets/Set Global Mipmap Bias -0.50", false, 1011)]
	static void SetBias05() {
	    foreach (Texture texture in selection) {
	        string path = AssetDatabase.GetAssetPath(texture);
    	    (AssetImporter.GetAtPath(path) as TextureImporter).mipMapBias = -0.5f;
        	AssetDatabase.ImportAsset(path);
    	}
	}

	[MenuItem("Assets/Set Global Mipmap Bias +0.00", true)]
	static bool ValidateSetBias0() {
    	selection = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
    	return (selection.Length > 0);
	}

	[MenuItem("Assets/Set Global Mipmap Bias +0.00", false, 1011)]
	static void SetBias0() {
	    foreach (Texture texture in selection) {
	        string path = AssetDatabase.GetAssetPath(texture);
    	    (AssetImporter.GetAtPath(path) as TextureImporter).mipMapBias = 0f;
        	AssetDatabase.ImportAsset(path);
    	}
	}

	[MenuItem("Assets/Set Global Mipmap Bias -0.25", true)]
	static bool ValidateSetBias025() {
    	selection = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
    	return (selection.Length > 0);
	}
	
	[MenuItem("Assets/Set Global Mipmap Bias -0.25", false, 1011)]
	static void SetBias025() {
	    foreach (Texture texture in selection) {
	        string path = AssetDatabase.GetAssetPath(texture);
    	    (AssetImporter.GetAtPath(path) as TextureImporter).mipMapBias = -0.25f;
        	AssetDatabase.ImportAsset(path);
    	}
	}

}
// #endif