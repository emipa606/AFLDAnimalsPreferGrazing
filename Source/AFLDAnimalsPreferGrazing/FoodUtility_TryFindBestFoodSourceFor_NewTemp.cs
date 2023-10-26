using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace _AFLD_AnimalsPreferGrazing;

internal static class FoodUtility_TryFindBestFoodSourceFor_NewTemp
{
    [HarmonyPatch(typeof(FoodUtility))]
    [HarmonyPatch("TryFindBestFoodSourceFor_NewTemp")]
    private static class FoodUtility_TryFindBestFoodSourceFor_NewTempPatch
    {
        private static bool Prefix(ref bool __result, Pawn getter, Pawn eater, bool forceScanWholeMap,
            bool ignoreReservations, ref Thing foodSource, ref ThingDef foodDef, bool allowForbidden = false,
            bool calculateWantedStackCount = false)
        {
            var eatsPlants = eater.RaceProps.Eats(FoodTypeFlags.Plant);
            var eatsCorpses = _AFLD_AnimalsPreferGrazingMod.instance.Settings.alsoPerferCorpses &&
                              eater.RaceProps.Eats(FoodTypeFlags.Corpse);
            if (getter != eater || !eater.RaceProps.Animal || !eatsPlants && !eatsCorpses)
            {
                return true;
            }

            var reserved_plant_or_corpse_filter = new HashSet<Thing>();
            foreach (var item in GenRadial.RadialDistinctThingsAround(getter.Position, getter.Map, 2f, true))
            {
                if (item is not Pawn pawn || pawn == getter || !pawn.RaceProps.Animal || pawn.CurJob == null ||
                    pawn.CurJob.def != JobDefOf.Ingest || !pawn.CurJob.GetTarget(TargetIndex.A).HasThing)
                {
                    continue;
                }

                var thing = pawn.CurJob.GetTarget(TargetIndex.A).Thing;
                reserved_plant_or_corpse_filter.Add(thing);
            }

            bool FoodValidator(Thing t)
            {
                var stackCount = 1;
                var statValue = t.GetStatValue(StatDefOf.Nutrition);
                if (calculateWantedStackCount)
                {
                    stackCount = FoodUtility.WillIngestStackCountOf(eater, t.def, statValue);
                }

                return eater.WillEat_NewTemp(t, getter) && t.def.IsNutritionGivingIngestible && t.IngestibleNow &&
                       (getter.AnimalAwareOf(t) || forceScanWholeMap) &&
                       (ignoreReservations || getter.CanReserve(t, 10, stackCount));
            }

            bool MiscValidator(Thing t)
            {
                if (!FoodValidator(t))
                {
                    return false;
                }

                if (reserved_plant_or_corpse_filter.Contains(t))
                {
                    return false;
                }

                if (!t.IngestibleNow)
                {
                    return false;
                }

                return t.Position.InAllowedArea(getter) && getter.CanReserve(t);
            }

            bool CorpseValidator(Thing t)
            {
                return t is Corpse && MiscValidator(t);
            }

            bool PlantValidator(Thing t)
            {
                return t is Plant && MiscValidator(t);
            }

            var ignoreEntirelyForbiddenRegions = !allowForbidden && ForbidUtility.CaresAboutForbidden(getter, true) &&
                                                 getter.playerSettings is
                                                     { EffectiveAreaRestrictionInPawnCurrentMap: not null };
            var searchRegionsMax = 30;
            if (forceScanWholeMap)
            {
                searchRegionsMax = -1;
            }
            else if (getter.Faction == Faction.OfPlayer)
            {
                searchRegionsMax = 100;
            }

            if (eatsCorpses)
            {
                var closestIncludingCorpses = GenClosest.ClosestThingReachable(getter.Position, getter.Map,
                    ThingRequest.ForGroup(ThingRequestGroup.FoodSource), PathEndMode.OnCell,
                    TraverseParms.For(getter), 9999f, CorpseValidator, null, 0, searchRegionsMax, false,
                    RegionType.Set_Passable, ignoreEntirelyForbiddenRegions);
                if (closestIncludingCorpses != null)
                {
                    foodSource = closestIncludingCorpses;
                    foodDef = FoodUtility.GetFinalIngestibleDef(closestIncludingCorpses);
                    __result = true;
                    return false;
                }
            }

            if (!eatsPlants)
            {
                return true;
            }

            var closestIncludingPlants = GenClosest.ClosestThingReachable(getter.Position, getter.Map,
                ThingRequest.ForGroup(ThingRequestGroup.FoodSource), PathEndMode.OnCell,
                TraverseParms.For(getter), 9999f, PlantValidator, null, 0, searchRegionsMax, false,
                RegionType.Set_Passable, ignoreEntirelyForbiddenRegions);
            if (closestIncludingPlants == null)
            {
                return true;
            }

            foodSource = closestIncludingPlants;
            foodDef = FoodUtility.GetFinalIngestibleDef(closestIncludingPlants);
            __result = true;
            return false;
        }
    }
}