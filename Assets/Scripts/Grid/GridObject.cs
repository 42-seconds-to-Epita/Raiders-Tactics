namespace QuickStart.Grid
{
    public class GridObject
    {
        private bool hasUnitJustTest;

        public GridObject(bool hasUnitJustTest)
        {
            this.hasUnitJustTest = hasUnitJustTest;
        }

        public override string ToString()
        {
            return hasUnitJustTest.ToString();
        }
    }
}