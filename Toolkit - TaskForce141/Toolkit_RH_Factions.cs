using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwitchToolkit;
using TwitchToolkit.Incidents;
using TwitchToolkit.Store;
using Verse;

namespace Toolkit_RH_Factions
{
    public class Toolkit_RH_Factions
    {
    }

    public class MilitaryAidBase : IncidentHelper
    {
        public override bool IsPossible()
        {
            worker = new IncidentWorker_CallForAid();
            worker.def = IncidentDefOf.RaidFriendly;

            parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.AllyAssistance, Helper.AnyPlayerMap);

            IEnumerable<Faction> factionSearch = Find.FactionManager.AllFactions.Where(s => s.def.defName == factionDefName);

            Faction TaskForce141;

            if (factionSearch != null && factionSearch.Count() > 0)
            {
                TaskForce141 = factionSearch.ElementAt(0);
            }
            else
            {
                return false;
            }

            if (TaskForce141.PlayerRelationKind == FactionRelationKind.Hostile)
            {
                return false;
            }

            parms.faction = TaskForce141;
            parms.raidArrivalModeForQuickMilitaryAid = true;

            parms.points = DiplomacyTuning.RequestedMilitaryAidPointsRange.RandomInRange;

            return true;
        }

        public override void TryExecute()
        {
            worker.TryExecute(parms);
        }

        private IncidentWorker worker;
        private IncidentParms parms;

        public string factionDefName;
    }

    public class TaskForce141 : MilitaryAidBase
    {
        public TaskForce141()
        {
            this.factionDefName = "RH_141TaskForce";
        }
    }

    public class TheGhosts : MilitaryAidBase
    {
        public TheGhosts()
        {
            this.factionDefName = "RH_TheGhosts";
        }
    }

    public class UmbraCompany : MilitaryAidBase
    {
        public UmbraCompany()
        {
            this.factionDefName = "RH_UmbraCompany";
        }
    }

    public class MSF : MilitaryAidBase
    {
        public MSF()
        {
            this.factionDefName = "RH_MSF";
        }
    }
}
