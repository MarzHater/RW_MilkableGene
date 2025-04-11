using RimWorld;
using Verse;
using System;

namespace RimWorldMilking
{
    public class HediffCompProperties_Milkable : HediffCompProperties
    {
        public int milkIntervalDays = 2;
        public int milkAmount = 10;

        public HediffCompProperties_Milkable()
        {
            compClass = typeof(HediffCompMilkable);
        }
    }

    public class HediffCompMilkable : HediffComp
    {
        private int ticksUntilMilk;

        public HediffCompProperties_Milkable Props => (HediffCompProperties_Milkable)props;

        public override void CompPostMake()
        {
            base.CompPostMake();
            ticksUntilMilk = Props.milkIntervalDays * 60000;
        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            if (ticksUntilMilk > 0)
            {
                ticksUntilMilk--;
            }
            else
            {
                if (Pawn.IsColonistPlayerControlled && Pawn.Spawned)
                {
                    Thing milk = ThingMaker.MakeThing(ThingDef.Named("StrangeMilk"));
                    milk.stackCount = Props.milkAmount;
                    GenPlace.TryPlaceThing(milk, Pawn.Position, Pawn.Map, ThingPlaceMode.Near);
                    ticksUntilMilk = Props.milkIntervalDays * 60000;
                }
            }
        }

        public override string CompLabelInBracketsExtra => ticksUntilMilk > 0
            ? $"Next milk in: {ticksUntilMilk / 60000f:0.0} days"
            : "Ready to milk!";
    }
}