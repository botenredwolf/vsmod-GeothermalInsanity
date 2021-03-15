using System;
using System.Collections.Generic;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Server;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;

[assembly: ModInfo("geothermalinsanity")]

namespace GeothermalInsanity {
  public class GeothermalInsanityMod : ModSystem {
    public override void Start(ICoreAPI api) {
      api.Event.OnGetClimate += (ref ClimateCondition climate, BlockPos pos, EnumGetClimateMode mode, double totalDays) => {
        climate.Temperature += api.World.SeaLevel - pos.Y;
      };
    }
  }
}