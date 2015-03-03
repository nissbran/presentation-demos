namespace DemoLibUnitTest.Customizations
{
    using Ploeh.AutoFixture.Kernel;

    public static class FixtureExtensions
    {
        public static T CreateWithRange<T>(this ISpecimenBuilder builder, T minimum, T maximum)
        {
            var context = new SpecimenContext(builder);

            return (T)context.Resolve(new RangedNumberRequest(typeof(T), minimum, maximum));
        }
    }
}
