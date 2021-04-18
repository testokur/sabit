using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace TestOkur.Testing
{
    public class TestOkurAutoDataAttribute : AutoDataAttribute
    {
        public TestOkurAutoDataAttribute()
            : base(() => new Fixture()
                .Customize(new AutoMoqCustomization()))
        {
        }
    }
}
