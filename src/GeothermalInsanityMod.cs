using System;
using System.Collections.Generic;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Server;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;

namespace GeothermalInsanity {
  public class GeothermalInsanityMod : ModSystem {
	public override double ExecuteOrder() {
      //has it execute after in-game engine fuckery to override variations
	  return 999;
	}
    public override void Start(ICoreAPI api) {
		//quixjote's key to make it actually fucking work
		base.Start(api);
		//some witchcraft from goxmeor to start this all
    	api.Event.OnGetClimate += (ref ClimateCondition climate, BlockPos pos, EnumGetClimateMode mode, double totalDays) => {
			//define some shit we need for intermediary
			float subDepth = 0, blockAbove = 0, genDifference = 0;
			bool isUnderground = false;
			var rainMapHeight = api.World.BlockAccessor.GetRainMapHeightAt(pos);
			var genHeight = api.World.BlockAccessor.GetTerrainMapheightAt(pos);
			//checks below gen height, assigns level difference
			if (pos.Y < genHeight){
				genDifference = (genHeight - pos.Y);
			}
			//attempts to figure out if there's an impermeable block somewhere above your head (are you under cover?)
			if (pos.Y < rainMapHeight){
				blockAbove = (rainMapHeight - pos.Y);
			}
			//checks to confirm both below surface and below gen height (properly underground)
			if (genDifference > 2 && blockAbove > 2){
				isUnderground = true;
			}
			//underground? how deep below surface and sea level?
			if (isUnderground == true){
				subDepth = (Math.Min(genDifference, blockAbove) + 2);
			}
			//doing the actual temperature shifts
			if (subDepth > 0) {
				climate.Temperature = 5 + (subDepth / 2.5f);
			}
   		};
      }
    }
  }