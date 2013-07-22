using System;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using BeatDown.Game;
using BeatDown.Game.Planning;
using System.Collections.Generic;

namespace BeatDown.Renderer.GameObjects
{
	public class UnitRenderer:BaseRender
	{
		public static void RenderViewable (Unit u)
		{
			if (u.Health < 0) {
				return;
			}

			//Console.WriteLine(String.Format("Drawunit{0} {1} == {2} {3} != {4} = {5}",SharedResources.MouseIsDown,u.glId ,Game.Selection.SelectedId ,Game.Selection.Maploc , Game.Selection.NONE, SharedResources.MouseIsDown && u.glId == Game.Selection.SelectedId && Game.Selection.Maploc != Game.Selection.NONE));
			//draw the path to the hovered location.
			if (SharedResources.MouseIsDown && 
				u.glId == Game.Selection.SelectedId 
				&& Game.Selection.Maploc != Game.Selection.NONE) {

				/*foreach(ITask task in u.Plan){
					if(task is Path){
						Path p = (Path)task;

						GL.PushMatrix();
						GL.Color3(1.0f,0.0f,0.0f);
						for(int i = 1; i < p.Coords.Count;i++){
							GL.Begin(BeginMode.Lines);

							GL.Vertex3(p.Coords[i].X,p.Coords[i].Y, p.Coords[i].Z);
							GL.Vertex3(p.Coords[i-1].X,p.Coords[i-1].Y, p.Coords[i-1].Z);



							GL.End ();
						}
						GL.PopMatrix();
					}
				}*/
			
				List<Coords> Coords = Render.Instance.theGame.Manager.World.GetPath(u.X, u.Z, Game.Selection.MapX,Game.Selection.MapZ);
				//Console.WriteLine("drawpath to "+x+","+z+" is "+Coords.Count);
				GL.PushMatrix();
					GL.Translate(0,1,0);
				if(Coords.Count> u.ActionPoints){
					GL.Color3(1.0f,0.0f,0.0f);
				}
				else{
						GL.Color3(0.0f,1.0f,0.0f);
				}
				for(int i = 1; i < Coords.Count;i++){
							
					//	Console.WriteLine(String.Format("Lines: {0},{1},{2}",Coords[i].X,Coords[i].Y, Coords[i].Z));
					GL.Begin(BeginMode.Lines);
							GL.Vertex3(Coords[i].X,Coords[i].Y, Coords[i].Z);
							GL.Vertex3(Coords[i-1].X,Coords[i-1].Y, Coords[i-1].Z);
					GL.End ();
				}

				GL.PopMatrix();	



			}




			GL.PushMatrix ();
			{
				GL.Translate (u.X, u.Y, u.Z);
				GL.Rotate ((float)u.Rotation, Renderer.Render.UP);
				GL.Scale (.75f, .75f, .75f);
				GL.Translate (u.SizeX / -2f, 0f, u.SizeZ / -2f);
					
						
				GL.Begin (BeginMode.QuadStrip);
				{
					GL.Color3 (Game.Game.Instance.Manager.GetTeamColor(u.Team));

					GL.Vertex3 (0, 0, 0);
					GL.Vertex3 (0, u.SizeY, 0);

					GL.Vertex3 (u.SizeX, 0, 0);
					GL.Vertex3 (u.SizeX, u.SizeY, 0);
								

					GL.Vertex3 (u.SizeX, 0, u.SizeZ);
					GL.Vertex3 (u.SizeX, u.SizeY, u.SizeZ);


					GL.Vertex3 (0, 0, u.SizeZ);
					GL.Vertex3 (0, u.SizeY, u.SizeZ);

					GL.Vertex3 (0, 0, 0);
					GL.Vertex3 (0, u.SizeY, 0);
				}

				GL.End ();

				GL.Begin (BeginMode.Quads);
				{
					GL.Vertex3 (0, u.SizeY, 0);
					GL.Vertex3 (0, u.SizeY, u.SizeZ);
					GL.Vertex3 (u.SizeX, u.SizeY, u.SizeZ);
					GL.Vertex3 (u.SizeX, u.SizeY, 0);
				}
				GL.End ();


				if (u.glId == Game.Selection.SelectedId) {

					GL.Color3(System.Drawing.Color.PaleVioletRed);
					GL.Begin(BeginMode.Lines);

					GL.Vertex3 (0, 0, 0);
					GL.Vertex3 (u.SizeX, 0, 0);
					GL.Vertex3 (u.SizeX, 0, u.SizeZ);
					GL.Vertex3 (0, 0, u.SizeZ);
					GL.Vertex3 (0,0,0);
					GL.End ();

				}
			}
			WeaponRenderer.RenderViewable(u.Weapon);

			GL.PopMatrix();
		}



		public static void RenderPickable(Unit u){
			BaseRender.RenderPickable(u);
		}
	}
}

