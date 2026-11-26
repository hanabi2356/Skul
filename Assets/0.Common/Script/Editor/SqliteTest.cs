using UnityEditor;
using UnityEngine;

public class SqliteTest : EditorWindow
{
	[MenuItem("Tools/DB Test")]
	private static void Test()
	{
		using var c = new Microsoft.Data.Sqlite.SqliteConnection("Data Source=Assets/Data/GameData.db");
		c.Open();
		using var cmd = c.CreateCommand();
		cmd.CommandText = "SELECT sqlite_version()";
		Debug.Log(cmd.ExecuteScalar());
	}

}
