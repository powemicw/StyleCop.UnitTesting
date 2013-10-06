namespace CustomRules.Test
{
    using CustomRulesSample;

    using NUnit.Framework;

    using StyleCopNUnit;

    [TestFixture]
    public class SampleTest : StyleCopTest
    {
        [Test]
        public void VerifySampleTest()
        {
            this.AddSourceCode("SourceCode/Invalid.cs");

            this.StartAnalysis();

            this.AssertViolated(RuleNames.SampleRule, new[] { 5 });
        }

        [Test]
        public void VerifySampleTest2()
        {
            const string Source = @"
                                        public class Invalid {
                                            var test = string.Empty;
                                        }
                                  ";

            this.AddSourceCode("SourceCode/Invalid2.cs", Source);

            this.StartAnalysis();

            this.AssertViolated(RuleNames.SampleRule, new[] { 2 });
        }

    }
}
