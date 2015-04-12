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

        public static string CreateConstrainedString(this ISpecimenBuilder builder, int length)
        {
            var context = new SpecimenContext(builder);

            return (string)context.Resolve(new ConstrainedStringRequest(length, length));
        }
    }
}
