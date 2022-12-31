using System;
using System.Collections.Generic;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace SpaceCore
{
    /// <summary>The SpaceCore API which other mods can access.</summary>
    public interface ISpaceCoreAPI
    {
        /*********
        ** Methods
        *********/
        /// <summary>Register a type as being valid for the vanilla serializer.</summary>
        /// <param name="type">Must have the attribute XmlType applied, with the parameter starting with "Mods_", ie. [XmlType("Mods_AuthorName_MyCustomObject")].</param>
        void RegisterSerializerType(Type type);
    }
}
