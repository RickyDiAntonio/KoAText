using System;
using System.Collections.Generic;
using static KoAText.Constants;

namespace KoAText.Towns
{
    public class TownInfo
    {
        public string Name { get; }
        public string Art { get; }

        public TownInfo(string name, string art)
        {
            Name = name;
            Art = art;
        }
    }

    public static class TownLibrary
    {
        public static readonly TownInfo Riverwood = new TownInfo("Riverwood", asciiDrawing.riverwoodArt);
        public static readonly TownInfo Stonevale = new TownInfo("Stonevale", asciiDrawing.StonevaleArt);
        public static readonly TownInfo Emberfall = new TownInfo("Emberfall", asciiDrawing.EmberfallArt);

        public static List<TownInfo> All = new() { Riverwood, Stonevale, Emberfall };
    }
}