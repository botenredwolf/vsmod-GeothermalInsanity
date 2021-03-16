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
	public override double ExecuteOrder() {
      //has it execute after in-game engine fuckery to override variations
	  return 999;
    }
    public override void Start(ICoreAPI api) {
		//some witchcraft from goxmeor to start this all
    	api.Event.OnGetClimate += (ref ClimateCondition climate, BlockPos pos, EnumGetClimateMode mode, double totalDays) => {
			//define some shit we need for intermediary
			float subDepth = 0, seaLevelDifference = 0, blockAbove = 0;
			bool isUnderground = true;
			var rainMapHeight = api.World.BlockAccessor.GetRainMapHeightAt(pos);
			//checks if you're below sea level, assigns level difference
			if (pos.Y < api.World.SeaLevel) {
				seaLevelDifference = (api.World.SeaLevel - pos.Y);
			}
			//attempts to figure out if there's an impermeable block somewhere above your head (are you underground?)
			if (pos.Y < rainMapHeight){
				isUnderground = true;
				blockAbove = (rainMapHeight - pos.Y);
			}
			//grabs the lower value of either depth below sea level or depth below surface as value for subDepth
			if (isUnderground == true && seaLevelDifference > 0){
				subDepth = Math.Min(seaLevelDifference, blockAbove);
			}
			//doing the actual temperature shifts
			if (subDepth > 0) {
				climate.Temperature = 5 + (subDepth / 2.5f);
			}
   		};
      }
    }
  }