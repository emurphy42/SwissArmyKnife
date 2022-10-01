using StardewModdingAPI;
using StardewValley;

namespace SwissArmyKnife
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        /*********
        ** Properties
        *********/
        /// <summary>The mod configuration from the player.</summary>
        private ModConfig Config;

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            this.Config = this.Helper.ReadConfig<ModConfig>();

            Helper.Events.GameLoop.DayStarted += (e, a) => SpawnSwissArmyKnife();
        }

        /*********
        ** Private methods
        *********/
        /// <summary>Spawn an item if needed and possible.</summary>
        private void SpawnSwissArmyKnife()
        {
            var swissArmyKnifeItemFactory = new SwissArmyKnifeItem(
                name: this.Config.SwissArmyKnifeName,
                description: this.Config.SwissArmyKnifeDescription,
                image: this.Config.SwissArmyKnifeImage,
                keypress: this.Config.SwissArmyKnifeKeypress,
                creator: this
            );
            foreach (var farmer in Game1.getOnlineFarmers())
            {
                if (farmer.hasItemInInventoryNamed(this.Config.SwissArmyKnifeName))
                {
                    continue;
                }
                if (!farmer.couldInventoryAcceptThisItem(swissArmyKnifeItemFactory))
                {
                    continue;
                }
                var swissArmyKnifeItem = swissArmyKnifeItemFactory.getOne();
                farmer.addItemToInventory(swissArmyKnifeItem);
                // this.Monitor.Log($"[Swiss Army Knife] Added item to {farmer.Name} inventory", LogLevel.Debug);
            }
        }
    }
}
