namespace DemoLibUnitTest.NUnit
{
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoNSubstitute;
    using Ploeh.AutoFixture.NUnit2;

    public class AutoNSubsituteData : AutoDataAttribute
    {
        public AutoNSubsituteData()
            : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
            Fixture.Customizations.Add(new RandomRangedNumberGenerator());
        }
    }
}
