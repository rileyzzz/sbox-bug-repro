
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace Sandbox
{
	/// <summary>
	/// This is your game class. This is an entity that is created serverside when
	/// the game starts, and is replicated to the client. 
	/// 
	/// You can use this to create things like HUDs and declare which player class
	/// to use for spawned players.
	/// </summary>
	/// 

	public partial class MyGame : Sandbox.Game
	{
		public MyGame()
		{

			if ( IsClient )
				CreateTestScene();
		}

		public override void PostLevelLoaded()
		{
			base.PostLevelLoaded();

		}

		/// <summary>
		/// A client has joined the server. Make them a pawn to play with
		/// </summary>
		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			// Create a pawn for this client to play with
			var pawn = new Pawn();
			client.Pawn = pawn;

			// Get all of the spawnpoints
			var spawnpoints = Entity.All.OfType<SpawnPoint>();

			// chose a random one
			var randomSpawnPoint = spawnpoints.OrderBy( x => Guid.NewGuid() ).FirstOrDefault();

			// if it exists, place the pawn there
			if ( randomSpawnPoint != null )
			{
				var tx = randomSpawnPoint.Transform;
				tx.Position = tx.Position + Vector3.Up * 50.0f; // raise it up
				pawn.Transform = tx;
			}
		}

		public override void Simulate( Client cl )
		{
			base.Simulate( cl );

			
			//Log.Info( $"{(IsServer ? "server" : "client")} test data {TestData[3]}");
		}

		private void CreateTestScene()
		{
			
			// create a few to exaggerate the issue
			for ( int i = 0; i < 3; i++ )
			{
				_ = new CrashSceneObject( Map.Scene )
				{
					Bounds = BBox.FromPositionAndSize( Vector3.Zero, 10.0f )
				};
			}

		}
	}

}
