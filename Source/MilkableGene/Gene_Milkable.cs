using Verse;
using RimWorld;

namespace RimWorldMilking
{
    public class Gene_Milkable : Gene
    {
        public override void PostAdd()
        {
            base.PostAdd();
            if (pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("HumanMilkableHediff")) == null)
            {
                pawn.health.AddHediff(HediffDef.Named("HumanMilkableHediff"));
            }
        }

        public override void PostRemove()
        {
            base.PostRemove();
            var hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("HumanMilkableHediff"));
            if (hediff != null)
                pawn.health.RemoveHediff(hediff);
        }
    }
}