namespace CustomizeIt.AI.Residential
{
    public partial class CustomizableResidentialBuildingAI : ResidentialBuildingAI, ICustomAI
    {
        public override void SimulationStep(ushort buildingID, ref Building buildingData, ref Building.Frame frameData) {
            if (m_isPloppable) buildingData.FixFlags();
            base.SimulationStep(buildingID, ref buildingData, ref frameData);
            if (m_isPloppable) buildingData.FixFlags();
        }

        protected override void SimulationStepActive(ushort buildingID, ref Building buildingData, ref Building.Frame frameData) {
            if (m_isPloppable) buildingData.FixFlags();
            base.SimulationStepActive(buildingID, ref buildingData, ref frameData);
            if (m_isPloppable) buildingData.FixFlags();
        }

        public override bool ClearOccupiedZoning() {
            if (m_isPloppable) return false;
            return base.ClearOccupiedZoning();
        }

        public override string GenerateName(ushort buildingID, InstanceID caller) {
            if (m_isPloppable) return m_info.GetUncheckedLocalizedTitle();
            return base.GenerateName(buildingID, caller);
        }

        public override BuildingInfo GetUpgradeInfo(ushort buildingID, ref Building data) {
            if (m_isPloppable) return null;
            return base.GetUpgradeInfo(buildingID, ref data);
        }

        public override void GetLengthRange(out int minLength, out int maxLength) {
            if (m_isPloppable) {
                minLength = 1;
                maxLength = 16;
            } else base.GetLengthRange(out minLength, out maxLength);
        }

        public override void GetWidthRange(out int minWidth, out int maxWidth) {
            if (m_isPloppable) {
                minWidth = 1;
                maxWidth = 16;
            } else base.GetWidthRange(out minWidth, out maxWidth);
        }
    }
}
