using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Utilities;
using StardewValley;
using System;
using System.Reflection;

namespace SwissArmyKnife
{
    public class SwissArmyKnifeItem : Tool
    {
		public new string description;
		private string image;
		private Texture2D texture;
		private SButton keypress;
		private ModEntry creator;

		public SwissArmyKnifeItem(string name, string description, string image, SButton keypress, ModEntry creator) : base(
			name: name,
			upgradeLevel: 0,
			initialParentTileIndex: 0,
			indexOfMenuItemView: 0,
			stackable: false
		)
		{
			this.description = description;
			this.image = image;
			this.texture = creator.Helper.ModContent.Load<Texture2D>(string.Format("assets/{0}.png", this.image));
			this.keypress = keypress;
			this.creator = creator;
			base.InstantUse = true;
		}

		public override Item getOne()
		{
			SwissArmyKnifeItem swissArmyKnife = new SwissArmyKnifeItem(
				name: base.BaseName,
				description: description,
				image: image,
				keypress: keypress,
				creator: creator
			);
			swissArmyKnife._GetOneFrom(this);
			return swissArmyKnife;
		}

		protected override string loadDescription()
		{
			return description;
		}

		protected override string loadDisplayName()
		{
			return base.BaseName;
		}

		public override void DoFunction(GameLocation location, int x, int y, int power, Farmer who)
        {
			who.CanMove = true;
			who.UsingTool = false;
			who.canReleaseTool = true;

			object inputStateGetter = creator.Helper.Input.GetType().GetField(
				name: "CurrentInputState",
				bindingAttr: BindingFlags.NonPublic | BindingFlags.Instance
			).GetValue(creator.Helper.Input);
			object inputState = (inputStateGetter as Delegate).DynamicInvoke();
			inputState.GetType().GetMethod("OverrideButton").Invoke(inputState, new object[] { keypress, true });
		}

		public override void drawInMenu(
			SpriteBatch spriteBatch,
			Vector2 location,
			float scaleSize,
			float transparency,
			float layerDepth,
			StackDrawType drawStackNumber,
            Microsoft.Xna.Framework.Color color,
			bool drawShadow
		)
        {
			spriteBatch.Draw(
				texture: texture,
				position: location,
				sourceRectangle: null,
				color: Microsoft.Xna.Framework.Color.White * transparency,
				rotation: 0,
				origin: Vector2.Zero,
				scale: scaleSize * 2,
				effects: SpriteEffects.None,
				layerDepth: layerDepth
			);
		}

	}
}
