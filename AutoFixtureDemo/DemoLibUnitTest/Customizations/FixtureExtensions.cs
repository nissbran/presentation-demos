namespace DemoLibUnitTest.Customizations
{
    using System;
    using System.Linq.Expressions;
    using Ploeh.AutoFixture.Dsl;
    using Ploeh.AutoFixture.Kernel;

    public static class FixtureExtensions
    {
        public static T CreateWithRange<T>(this ISpecimenBuilder builder, T minimum, T maximum)
        {
            var context = new SpecimenContext(builder);

            return (T)context.Resolve(new RangedNumberRequest(typeof(T), minimum, maximum));
        }

        public static string CreateStringWithPattern(this ISpecimenBuilder builder, string pattern)
        {
            var context = new SpecimenContext(builder);

            return (string)context.Resolve(new RegularExpressionRequest(pattern));
        }

        public static IPostprocessComposer<T> WithPattern<T>(this IPostprocessComposer<T> composer,
                                                           Expression<Func<T, object>> propertyPicker,
                                                           string pattern)
        {
            return composer.With(propertyPicker,
                                 new SpecimenContext(composer).Resolve(new RegularExpressionRequest(pattern)));
        }
    }
}
