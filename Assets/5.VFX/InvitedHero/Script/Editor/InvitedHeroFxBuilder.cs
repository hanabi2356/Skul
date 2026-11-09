using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public static class InvitedHeroFxBuilder
{
	private const string Root = "Assets/5.VFX/InvitedHero";
	private const float Ppu = 100f;
	private const float PixelScale = 3.5f;

	private struct Sheet
	{
		public string File;
		public int W;
		public int H;
		public int Cols;
		public int Rows;
		public int Ox;
		public int Oy;
		public float Duration;
		public bool Additive;
	}

	[MenuItem("Tools/Skul/Build Invited Hero FX")]
	public static void Build()
	{
		Directory.CreateDirectory(Path.GetFullPath(Root + "/Material"));
		Directory.CreateDirectory(Path.GetFullPath(Root + "/Prefab"));

		var slash = Load("crescent-slash", 128, 128, 4, 3, 64, 64, 0.55f, false);
		var wave = Load("crescent-wave", 96, 112, 4, 3, 48, 56, 0.6f, false);
		var line = Load("sword-line", 240, 48, 2, 6, 8, 24, 0.55f, false);
		var slam = Load("floor-slam", 144, 88, 3, 4, 72, 68, 0.55f, false);
		var bolt = Load("judgment-bolt", 72, 168, 6, 2, 36, 156, 0.55f, false);
		var orb = Load("mana-orb", 64, 64, 4, 3, 32, 32, 0.8f, false);
		var smoke = Load("mana-smoke", 48, 48, 4, 3, 24, 40, 1f, false);
		var spark = Load("pixel-spark", 16, 16, 1, 1, 8, 8, 1f, true);
		var glint = Load("pixel-glint", 16, 16, 1, 1, 8, 8, 1f, true);

		var slashDown = OneShot("FX_InvitedHero_SlashDown", slash, 0.9f, 0.7f);
		var slashUp = OneShot("FX_InvitedHero_SlashUp", slash, -0.9f, 0.7f);
		var dash = OneShot("FX_InvitedHero_DashSlash", slash, -1.15f, 0.7f, 1.15f);
		var flurry = Flurry(slash, spark);
		var swordWave = Wave(wave, spark);
		var storm = Storm(wave);
		var swordLine = Line(line, spark);
		var slamFx = Impact(slam, spark, "FX_InvitedHero_Slam");
		var lightning = Impact(bolt, spark, "FX_InvitedHero_Lightning");
		var orbFx = Orb(orb, glint);
		var smokeLow = Smoke("FX_InvitedHero_SmokeLow", smoke, new Vector3(-0.15f, 0.05f, 0f), new Vector2(-0.7f, -0.25f), new Vector2(0.05f, 0.25f));
		var smokeHigh = Smoke("FX_InvitedHero_SmokeHigh", smoke, new Vector3(0.35f, 0.15f, 0f), new Vector2(-0.1f, 0.15f), new Vector2(0.7f, 1.3f));
		var smokeWrap = SmokeWrap(smoke);

		var entries = new[]
		{
			Entry(InvitedHeroFxKind.SlashDown, slashDown),
			Entry(InvitedHeroFxKind.SlashUp, slashUp),
			Entry(InvitedHeroFxKind.Flurry, flurry),
			Entry(InvitedHeroFxKind.DashSlash, dash),
			Entry(InvitedHeroFxKind.SwordWave, swordWave),
			Entry(InvitedHeroFxKind.SwordStorm, storm),
			Entry(InvitedHeroFxKind.SwordLine, swordLine),
			Entry(InvitedHeroFxKind.Slam, slamFx),
			Entry(InvitedHeroFxKind.Lightning, lightning),
			Entry(InvitedHeroFxKind.Orb, orbFx),
			Entry(InvitedHeroFxKind.SmokeLow, smokeLow),
			Entry(InvitedHeroFxKind.SmokeWrap, smokeWrap),
			Entry(InvitedHeroFxKind.SmokeHigh, smokeHigh),
		};
		SavePlayer(entries);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		Debug.Log("Invited hero phase-1 particle prefabs built under " + Root);
	}

	private static Sheet Load(string file, int w, int h, int cols, int rows, int ox, int oy, float duration, bool additive)
	{
		var path = Root + "/Texture/" + file + ".png";
		var importer = (TextureImporter)AssetImporter.GetAtPath(path);
		importer.textureType = TextureImporterType.Default;
		importer.alphaIsTransparency = true;
		importer.mipmapEnabled = false;
		importer.wrapMode = TextureWrapMode.Clamp;
		importer.filterMode = FilterMode.Point;
		importer.textureCompression = TextureImporterCompression.Uncompressed;
		importer.npotScale = TextureImporterNPOTScale.None;
		importer.sRGBTexture = true;
		importer.maxTextureSize = 2048;
		importer.SaveAndReimport();

		var tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
		var matPath = Root + "/Material/M_" + file + ".mat";
		var mat = AssetDatabase.LoadAssetAtPath<Material>(matPath);
		var shader = Shader.Find("Universal Render Pipeline/Particles/Unlit");
		if (mat == null)
		{
			mat = new Material(shader);
			AssetDatabase.CreateAsset(mat, matPath);
		}
		else
		{
			mat.shader = shader;
		}

		mat.SetTexture("_BaseMap", tex);
		mat.SetColor("_BaseColor", Color.white);
		mat.SetFloat("_Surface", 1f);
		mat.SetFloat("_Blend", additive ? 2f : 0f);
		mat.SetFloat("_ZWrite", 0f);
		mat.SetFloat("_Cull", (float)CullMode.Off);
		mat.SetFloat("_SrcBlend", (float)BlendMode.SrcAlpha);
		mat.SetFloat("_DstBlend", additive ? (float)BlendMode.One : (float)BlendMode.OneMinusSrcAlpha);
		mat.SetFloat("_SrcBlendAlpha", (float)BlendMode.One);
		mat.SetFloat("_DstBlendAlpha", additive ? (float)BlendMode.One : (float)BlendMode.OneMinusSrcAlpha);
		mat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
		mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
		mat.DisableKeyword("_ALPHAMODULATE_ON");
		mat.SetOverrideTag("RenderType", "Transparent");
		mat.renderQueue = 3000;
		EditorUtility.SetDirty(mat);

		return new Sheet
		{
			File = file,
			W = w,
			H = h,
			Cols = cols,
			Rows = rows,
			Ox = ox,
			Oy = oy,
			Duration = duration,
			Additive = additive,
		};
	}

	private static Material Mat(string file)
	{
		return AssetDatabase.LoadAssetAtPath<Material>(Root + "/Material/M_" + file + ".mat");
	}

	private static GameObject OneShot(string name, Sheet sheet, float rotZ, float life, float sizeMul = 1f)
	{
		var root = NewRoot(name, life);
		var body = AddSheet(root, "Slash", sheet, Anchor(sheet), sheet.Duration, false, rotZ, 20, sizeMul);
		var sparks = AddStamp(root, "Sparks", "pixel-spark", 0.08f, 0.22f, 8, 0.06f, 18);
		Spray(sparks, rotZ);
		return Save(root, name);
	}

	private static GameObject Flurry(Sheet slash, Sheet spark)
	{
		var root = NewRoot("FX_InvitedHero_Flurry", 1.1f);
		var body = AddSheet(root, "Slashes", slash, Anchor(slash), slash.Duration, false, 0f, 20, 0.72f);
		var main = body.main;
		main.startRotation = new ParticleSystem.MinMaxCurve(-0.7f, 0.7f);
		var emission = body.emission;
		emission.rateOverTime = 0f;
		emission.SetBursts(new[]
		{
			new ParticleSystem.Burst(0.00f, 2),
			new ParticleSystem.Burst(0.06f, 2),
			new ParticleSystem.Burst(0.12f, 2),
			new ParticleSystem.Burst(0.18f, 2),
			new ParticleSystem.Burst(0.26f, 1),
		});
		var sparks = AddStamp(root, "Sparks", spark.File, 0.07f, 0.3f, 10, 0.02f, 16);
		var rate = sparks.emission;
		rate.rateOverTime = 0f;
		rate.SetBursts(new[] { new ParticleSystem.Burst(0.08f, (short)8) });
		var sparkMain = sparks.main;
		sparkMain.startLifetime = 0.4f;
		Spray(sparks, 0f);
		var shape = sparks.shape;
		shape.enabled = true;
		shape.shapeType = ParticleSystemShapeType.Circle;
		shape.radius = 0.25f;
		shape.arc = 140f;
		return Save(root, "FX_InvitedHero_Flurry");
	}

	private static GameObject Wave(Sheet wave, Sheet spark)
	{
		var root = NewRoot("FX_InvitedHero_SwordWave", 1.3f);
		var body = AddSheet(root, "Wave", wave, Anchor(wave), 1.15f, false, 0f, 20, 1f);
		var sheetAnim = body.textureSheetAnimation;
		sheetAnim.cycleCount = 2;
		PushX(body, 3.2f);
		var sparks = AddStamp(root, "Trail", spark.File, 0.07f, 0.35f, 12, 0f, 12);
		var sparkEmission = sparks.emission;
		sparkEmission.rateOverTime = 8f;
		sparkEmission.SetBursts(new ParticleSystem.Burst[0]);
		var vol = sparks.velocityOverLifetime;
		vol.enabled = true;
		vol.space = ParticleSystemSimulationSpace.Local;
		vol.x = new ParticleSystem.MinMaxCurve(-1.4f, -0.3f);
		vol.y = new ParticleSystem.MinMaxCurve(-0.35f, 0.35f);
		vol.z = new ParticleSystem.MinMaxCurve(0f, 0f);
		return Save(root, "FX_InvitedHero_SwordWave");
	}

	private static GameObject Storm(Sheet wave)
	{
		var root = NewRoot("FX_InvitedHero_SwordStorm", 1.35f);
		var body = AddSheet(root, "Waves", wave, Anchor(wave), 0.95f, false, 0f, 20, 0.85f);
		var sheetAnim = body.textureSheetAnimation;
		sheetAnim.cycleCount = 2;
		var main = body.main;
		main.startRotation = new ParticleSystem.MinMaxCurve(-0.45f, 0.45f);
		var emission = body.emission;
		emission.SetBursts(new[]
		{
			new ParticleSystem.Burst(0.02f, 2),
			new ParticleSystem.Burst(0.1f, 3),
			new ParticleSystem.Burst(0.18f, 2),
		});
		var vol = body.velocityOverLifetime;
		vol.enabled = true;
		vol.space = ParticleSystemSimulationSpace.Local;
		vol.x = new ParticleSystem.MinMaxCurve(2.4f, 4.4f);
		vol.y = new ParticleSystem.MinMaxCurve(-1.5f, 1.5f);
		vol.z = new ParticleSystem.MinMaxCurve(0f, 0f);
		return Save(root, "FX_InvitedHero_SwordStorm");
	}

	private static GameObject Line(Sheet line, Sheet spark)
	{
		var root = NewRoot("FX_InvitedHero_SwordLine", 0.8f);
		AddSheet(root, "Beam", line, Anchor(line), line.Duration, false, 0f, 20, 1f);
		var tip = AddStamp(root, "TipSparks", spark.File, 0.08f, 0.22f, 6, 0.08f, 16);
		tip.transform.localPosition = new Vector3(1.7f, 0f, 0f);
		PushX(tip, 1.2f);
		return Save(root, "FX_InvitedHero_SwordLine");
	}

	private static GameObject Impact(Sheet sheet, Sheet spark, string name)
	{
		var root = NewRoot(name, 0.85f);
		AddSheet(root, "Body", sheet, Anchor(sheet), sheet.Duration, false, 0f, 20, 1f);
		var sparks = AddStamp(root, "Sparks", spark.File, 0.08f, 0.24f, 8, 0.08f, 18);
		var vol = sparks.velocityOverLifetime;
		vol.enabled = true;
		vol.space = ParticleSystemSimulationSpace.Local;
		vol.x = new ParticleSystem.MinMaxCurve(-1.1f, 1.1f);
		vol.y = new ParticleSystem.MinMaxCurve(0.4f, 1.8f);
		vol.z = new ParticleSystem.MinMaxCurve(0f, 0f);
		var sparkMain = sparks.main;
		sparkMain.gravityModifier = 0.6f;
		return Save(root, name);
	}

	private static GameObject Orb(Sheet orb, Sheet glint)
	{
		var root = NewRoot("FX_InvitedHero_Orb", 0f);
		var body = AddSheet(root, "Orb", orb, Vector3.zero, 4f, true, 0f, 22, 1.7f);
		var sheetAnim = body.textureSheetAnimation;
		sheetAnim.cycleCount = 5;
		var motes = AddStamp(root, "Motes", glint.File, 0.16f, 4f, 8, 0f, 24);
		var moteMain = motes.main;
		moteMain.loop = true;
		moteMain.duration = 4f;
		var moteEmission = motes.emission;
		moteEmission.rateOverTime = 6f;
		moteEmission.SetBursts(new ParticleSystem.Burst[0]);
		var vol = motes.velocityOverLifetime;
		vol.enabled = true;
		vol.space = ParticleSystemSimulationSpace.Local;
		vol.orbitalX = new ParticleSystem.MinMaxCurve(0f, 0f);
		vol.orbitalY = new ParticleSystem.MinMaxCurve(0f, 0f);
		vol.orbitalZ = new ParticleSystem.MinMaxCurve(1.4f, 1.8f);
		vol.radial = new ParticleSystem.MinMaxCurve(0.15f, 0.45f);
		var shape = motes.shape;
		shape.enabled = true;
		shape.shapeType = ParticleSystemShapeType.Circle;
		shape.radius = 0.28f;
		shape.arc = 360f;
		return Save(root, "FX_InvitedHero_Orb");
	}

	private static GameObject Smoke(string name, Sheet smoke, Vector3 pos, Vector2 vx, Vector2 vy)
	{
		var root = NewRoot(name, 1.3f);
		var body = AddSheet(root, "Puffs", smoke, pos + Anchor(smoke), 0.7f, false, 0f, 8, 1f);
		var puffMain = body.main;
		puffMain.startLifetime = 0.65f;
		puffMain.duration = 0.55f;
		var puffEmission = body.emission;
		puffEmission.rateOverTime = 9f;
		puffEmission.SetBursts(new ParticleSystem.Burst[0]);
		var vol = body.velocityOverLifetime;
		vol.enabled = true;
		vol.space = ParticleSystemSimulationSpace.Local;
		vol.x = new ParticleSystem.MinMaxCurve(vx.x, vx.y);
		vol.y = new ParticleSystem.MinMaxCurve(vy.x, vy.y);
		vol.z = new ParticleSystem.MinMaxCurve(0f, 0f);
		return Save(root, name);
	}

	private static GameObject SmokeWrap(Sheet smoke)
	{
		var root = NewRoot("FX_InvitedHero_SmokeWrap", 1.3f);
		var body = AddSheet(root, "Wrap", smoke, Anchor(smoke), 0.7f, false, 0f, 8, 0.85f);
		var wrapMain = body.main;
		wrapMain.startLifetime = 0.6f;
		wrapMain.duration = 0.55f;
		var wrapEmission = body.emission;
		wrapEmission.rateOverTime = 14f;
		wrapEmission.SetBursts(new ParticleSystem.Burst[0]);
		var shape = body.shape;
		shape.enabled = true;
		shape.shapeType = ParticleSystemShapeType.Circle;
		shape.radius = 0.42f;
		shape.radiusThickness = 0.2f;
		shape.arc = 360f;
		var vol = body.velocityOverLifetime;
		vol.enabled = true;
		vol.space = ParticleSystemSimulationSpace.Local;
		vol.orbitalX = 0f;
		vol.orbitalY = 0f;
		vol.orbitalZ = 1.4f;
		vol.x = 0f;
		vol.y = 0.15f;
		vol.z = 0f;
		return Save(root, "FX_InvitedHero_SmokeWrap");
	}

	private static GameObject NewRoot(string name, float life)
	{
		var root = new GameObject(name);
		root.transform.localScale = new Vector3(PixelScale, PixelScale, PixelScale);
		if (life > 0f)
		{
			var lifetime = root.AddComponent<InvitedHeroFxLifetime>();
			var so = new SerializedObject(lifetime);
			so.FindProperty("_life").floatValue = life;
			so.ApplyModifiedPropertiesWithoutUndo();
		}
		return root;
	}

	private static Vector3 Anchor(Sheet s)
	{
		return new Vector3((s.W * 0.5f - s.Ox) / Ppu, (s.Oy - s.H * 0.5f) / Ppu, 0f);
	}

	private static ParticleSystem AddSheet(GameObject root, string name, Sheet sheet, Vector3 localPos, float life, bool loop, float rotZ, int order, float sizeMul)
	{
		var go = new GameObject(name);
		go.transform.SetParent(root.transform, false);
		go.transform.localPosition = localPos;
		var ps = go.AddComponent<ParticleSystem>();
		var main = ps.main;
		main.playOnAwake = true;
		main.loop = loop;
		main.duration = Mathf.Max(life, sheet.Duration);
		main.startLifetime = life;
		main.startSpeed = 0f;
		main.startSize3D = true;
		main.startSizeX = sheet.W / Ppu * sizeMul;
		main.startSizeY = sheet.H / Ppu * sizeMul;
		main.startRotation = rotZ;
		main.scalingMode = ParticleSystemScalingMode.Hierarchy;
		main.simulationSpace = ParticleSystemSimulationSpace.Local;
		main.maxParticles = 24;
		main.stopAction = ParticleSystemStopAction.None;

		var emission = ps.emission;
		emission.rateOverTime = 0f;
		emission.SetBursts(new[] { new ParticleSystem.Burst(0f, 1) });

		if (sheet.Cols * sheet.Rows > 1)
		{
			var sheetAnim = ps.textureSheetAnimation;
			sheetAnim.enabled = true;
			sheetAnim.mode = ParticleSystemAnimationMode.Grid;
			sheetAnim.numTilesX = sheet.Cols;
			sheetAnim.numTilesY = sheet.Rows;
			sheetAnim.animation = ParticleSystemAnimationType.WholeSheet;
			sheetAnim.cycleCount = 1;
			sheetAnim.frameOverTime = new ParticleSystem.MinMaxCurve(1f, AnimationCurve.Linear(0f, 0f, 1f, 1f));
		}

		var renderer = ps.GetComponent<ParticleSystemRenderer>();
		renderer.sharedMaterial = Mat(sheet.File);
		renderer.renderMode = ParticleSystemRenderMode.Billboard;
		renderer.alignment = ParticleSystemRenderSpace.View;
		renderer.sortingOrder = order;
		renderer.shadowCastingMode = ShadowCastingMode.Off;
		renderer.receiveShadows = false;
		return ps;
	}

	private static ParticleSystem AddStamp(GameObject root, string name, string file, float size, float life, int count, float delay, int order)
	{
		var go = new GameObject(name);
		go.transform.SetParent(root.transform, false);
		var ps = go.AddComponent<ParticleSystem>();
		var main = ps.main;
		main.playOnAwake = true;
		main.loop = false;
		main.duration = life + delay + 0.05f;
		main.startLifetime = life;
		main.startSpeed = 0f;
		main.startSize = size;
		main.scalingMode = ParticleSystemScalingMode.Hierarchy;
		main.simulationSpace = ParticleSystemSimulationSpace.Local;
		main.maxParticles = Mathf.Max(count, 16);
		main.stopAction = ParticleSystemStopAction.None;
		var emission = ps.emission;
		emission.rateOverTime = 0f;
		emission.SetBursts(new[] { new ParticleSystem.Burst(delay, (short)count) });
		var color = ps.colorOverLifetime;
		color.enabled = true;
		var gradient = new Gradient();
		gradient.SetKeys(
			new[]
			{
				new GradientColorKey(Color.white, 0f),
				new GradientColorKey(new Color(0.75f, 0.88f, 1f), 1f),
			},
			new[]
			{
				new GradientAlphaKey(1f, 0f),
				new GradientAlphaKey(1f, 0.35f),
				new GradientAlphaKey(0f, 1f),
			});
		color.color = gradient;
		var renderer = ps.GetComponent<ParticleSystemRenderer>();
		renderer.sharedMaterial = Mat(file);
		renderer.renderMode = ParticleSystemRenderMode.Billboard;
		renderer.alignment = ParticleSystemRenderSpace.View;
		renderer.sortingOrder = order;
		renderer.shadowCastingMode = ShadowCastingMode.Off;
		renderer.receiveShadows = false;
		return ps;
	}

	private static void Spray(ParticleSystem ps, float rotZ)
	{
		var vol = ps.velocityOverLifetime;
		vol.enabled = true;
		vol.space = ParticleSystemSimulationSpace.Local;
		var dir = new Vector2(Mathf.Cos(rotZ), Mathf.Sin(rotZ));
		vol.x = new ParticleSystem.MinMaxCurve(dir.x * 0.15f, dir.x * 0.7f + 0.15f);
		vol.y = new ParticleSystem.MinMaxCurve(dir.y * 0.15f - 0.15f, dir.y * 0.6f + 0.2f);
		vol.z = new ParticleSystem.MinMaxCurve(0f, 0f);
	}

	private static void PushX(ParticleSystem ps, float speed)
	{
		var vol = ps.velocityOverLifetime;
		vol.enabled = true;
		vol.space = ParticleSystemSimulationSpace.Local;
		vol.x = speed;
		vol.y = 0f;
		vol.z = 0f;
	}

	private static InvitedHeroFxPlayer.Entry Entry(InvitedHeroFxKind kind, GameObject prefab)
	{
		return new InvitedHeroFxPlayer.Entry { kind = kind, prefab = prefab };
	}

	private static void SavePlayer(InvitedHeroFxPlayer.Entry[] entries)
	{
		var root = new GameObject("InvitedHeroFxPlayer");
		var player = root.AddComponent<InvitedHeroFxPlayer>();
		var so = new SerializedObject(player);
		var list = so.FindProperty("_entries");
		list.arraySize = entries.Length;
		for (var i = 0; i < entries.Length; i++)
		{
			var element = list.GetArrayElementAtIndex(i);
			element.FindPropertyRelative("kind").enumValueIndex = (int)entries[i].kind;
			element.FindPropertyRelative("prefab").objectReferenceValue = entries[i].prefab;
		}
		so.ApplyModifiedPropertiesWithoutUndo();
		Save(root, "InvitedHeroFxPlayer");
	}

	private static GameObject Save(GameObject root, string name)
	{
		var path = Root + "/Prefab/" + name + ".prefab";
		var prefab = PrefabUtility.SaveAsPrefabAsset(root, path);
		Object.DestroyImmediate(root);
		return prefab;
	}
}
