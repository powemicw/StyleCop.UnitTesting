StyleCop.UnitTesting
====================

NUnit helperclass to simplify unit testing of custom StyleCop rules

##Installation

https://www.nuget.org/packages/StyleCop.NUnit/

    Install-Package StyleCop.NUnit

##Usage

Inherit your TestFixture class from the StyleCopNUnit.StyleCopTest to get access to the helper methods used below.

    using NUnit.Framework;
    using StyleCopNUnit;


    [TestFixture]
    public class SampleTest : StyleCopTest
    {
        [Test]
        public void AvoidUsingFieldHasValue()
        {
            this.AddSourceCode("Invalid.cs");

            this.StartAnalysis();

            this.AssertViolated("[Failing rule name]", new[] { 9, 10 });
        }
    }
    
##Credits

Most of the source code comes from this link, with some changes pointed out in the comments to get it working with later versions of StyleCop.

http://shishkin.wordpress.com/2008/05/30/stylecop-test-driven-rules-development/
