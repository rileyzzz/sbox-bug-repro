
using Sandbox;

public class CrashSceneObject : SceneCustomObject
{
	Texture ColorTarget = null;
	Texture DepthTarget = null;
	RenderAttributes SceneAttributes;

	public CrashSceneObject(SceneWorld world)
		: base(world)
	{
		ColorTarget = Texture.CreateRenderTarget().WithSize( 512, 512 ).WithFormat( ImageFormat.RGBA16161616F ).Create();
		DepthTarget = Texture.CreateRenderTarget().WithSize( 512, 512 ).WithDepthFormat().Create();

		SceneAttributes = new RenderAttributes();
	}

	public override void RenderSceneObject()
	{
		bool mainRenderPass = Render.Viewport.width != 512;

		if ( mainRenderPass )
		{
			Log.Info( "rendering" );
			Render.Draw.DrawScene( ColorTarget, DepthTarget, World, SceneAttributes, new Rect( 0, 0, 512, 512 ), Vector3.Random * 500.0f, Rotation.Random, 80.0f );
		}

		base.RenderSceneObject();
	}
}
