namespace StyleCopNUnit
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using NUnit.Framework;

    using StyleCop;

    public abstract class StyleCopTest
    {
        private CodeProject project;

        protected StyleCopTest()
        {
            var settings = Path.GetFullPath("Settings.StyleCop");
            var addinPaths = new List<string> { AppDomain.CurrentDomain.BaseDirectory };
            this.Console = new StyleCopConsole(settings, false, null, addinPaths, true);

            this.Console.ViolationEncountered += (sender, args) => this.Violations.Add(args.Violation);
            this.Console.OutputGenerated += (sender, args) => this.Output.Add(args.Output);
        }

        public StyleCopConsole Console { get; private set; }

        public List<string> Output { get; private set; }

        public List<Violation> Violations { get; private set; }

        [SetUp]
        public void Setup()
        {
            this.Violations = new List<Violation>();
            this.Output = new List<string>();

            var configuration = new Configuration(new string[0]);

            string location = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.StyleCop"); 
            this.project = new CodeProject(Guid.NewGuid().GetHashCode(), location, configuration);
        }

        [TearDown]
        public void TearDown()
        {
            this.project = null;
        }

        public void AddSourceCode(string fileName)
        {
            fileName = Path.GetFullPath(fileName);
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("SourceCode not found: " + fileName);
            }

            this.Console.Core.Environment.AddSourceCode(this.project, fileName, null);
        }

        public void AddSourceCode(string fileName, string sourceCode)
        {
            File.WriteAllText(Path.GetFullPath(fileName), sourceCode);

            this.AddSourceCode(fileName);
        }


        public void StartAnalysis()
        {
            var projects = new[] { this.project };
            this.Console.Start(projects, true);
        }

        public void AssertNotViolated(string ruleName)
        {
            if (this.Violations.Exists(x => x.Rule.Name == ruleName))
            {
                Assert.Fail("False positive for rule {0}.", ruleName);
            }
        }

        public void AssertViolated(string ruleName, int[] lineNumbers)
        {
            if (lineNumbers != null && lineNumbers.Length > 0)
            {
                foreach (var lineNumber in lineNumbers)
                {
                    if (!this.Violations.Exists(x =>
                        x.Rule.Name == ruleName &&
                        x.Line == lineNumber))
                    {
                        Assert.Fail(
                            "Failed to violate rule {0} on line {1}.",
                            ruleName,
                            lineNumber);
                    }
                }
            }
        }
    }
}
