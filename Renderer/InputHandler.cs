using System;
using OpenTK.Input;
using BeatDown.Game;


namespace BeatDown.Renderer
{
	public class InputHandler
	{
	

		public static void OnMouseUp (object sender, MouseButtonEventArgs args)
		{
			SharedResources.MouseIsDown = false;

			if(Renderer.Render.Instance.gui.WasClicked(args.X, args.Y)){
				Renderer.Render.Instance.gui.MouseUp(args);
			}

			if (Game.State.States.INGAME != Game.Game.State.Current) {
				return;
			}

		//	Console.WriteLine(String.Format("Mouseup {0} {1} {2}",Game.Selection.HoveredId, Game.Selection.Maploc, Game.Selection.SelectedId));

			//select with left click
			if (args.Button == MouseButton.Left) {
				if(Game.Selection.SelectedId != Game.Selection.HoveredId){
					Game.Selection.SelectedId = Game.Selection.HoveredId;
					//TODO SELECTION EVENT?
				}
			}
			//act with right click
			if (args.Button == MouseButton.Right) {
				if (Game.Selection.SelectedId != Game.Selection.NONE) {

					Unit selected = Game.Game.Instance.Manager.Units [Game.Selection.SelectedId];

					//only allow orders to ones own team
					if(Game.Game.Instance.LocalPlayer.Team == selected.Team){
						//aonly let the current person  issue orders
					   if(Game.Game.Instance.LocalPlayer.Id == Game.Game.Instance.WhoseTurn.Id){
							if (Game.Selection.HoveredId == Game.Selection.NONE ) {
								//move order
								if( Game.Selection.Maploc > 0){
									//TODO can move to.here
									BeatDown.Game.Planning.Movement m = new Game.Planning.Movement(selected, 
								                           Render.Instance.theGame.Manager.World.GetPath(
																selected.X, 
																selected.Z, 
																Game.Selection.MapX,
																Game.Selection.MapZ)
									                                        );
									selected.AddTask(m,Render.Instance.Keyboard[Key.ShiftLeft]);

								}
							} else {
								//did we click a unit?
								if(Game.Selection.HoveredId != Game.Selection.NONE){
									Unit target = Game.Game.Instance.Manager.Units [Game.Selection.HoveredId];
								
									if(target.Team == selected.Team){
										//aid?

									}
									else{
										//attack?
										if(selected.CanAttack(target)){
											selected.AddTask(new BeatDown.Game.Planning.Attack(selected,target));
											//target.TakeDamage(selected.Weapon.MaxDamage);
											new Resources.Texture(SharedResources.InGameFont, selected.Weapon.MaxDamage.ToString(), System.Drawing.Brushes.Red, 24,24);								}

									}
								}
							}
						}
					}
				}
			}


		}

		public static void OnMouseDown (object sender, MouseButtonEventArgs args)
		{
			//switch( Render.Instance.theGame.State//
			SharedResources.MouseIsDown = true;
			

		}

		public static void OnMouseClick(object sender, MouseButtonEventArgs args){

		}

		public static void OnMouseWheeled(object sender, MouseWheelEventArgs args){
		//	Renderer.Render.Instance.gui.Input.ProcessMouseMessage(args);

		}

		public static void OnKeyDown(object sender, KeyboardKeyEventArgs e){
		//	Renderer.Render.Instance.gui.Input.ProcessKeyDown(e);
		}

		public static void OnKeyUp (object sender, KeyboardKeyEventArgs e){
		//	Renderer.Render.Instance.gui.Input.ProcessKeyUp(e);

		}

	}
}

