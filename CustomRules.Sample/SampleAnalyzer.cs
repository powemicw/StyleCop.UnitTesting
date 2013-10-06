namespace CustomRulesSample
{
    using StyleCop;
    using StyleCop.CSharp;

    [SourceAnalyzer(typeof(CsParser))]
    public class SampleAnalyzer : SourceAnalyzer
    {
        /// <summary>
        /// Extremely simple analyzer for demo purposes.
        /// </summary>
        public override void AnalyzeDocument(CodeDocument document)
        {
            var doc = (CsDocument)document;

            // skipping wrong or auto-generated documents
            if (doc.RootElement == null || doc.RootElement.Generated)
            {
                return;
            }

            // check all class entries
            doc.WalkDocument(this.CheckClasses);
        }

        /// <summary>
        /// Checks whether specified element conforms custom rule CR0001.
        /// </summary>
        private bool CheckClasses(
            CsElement element,
            CsElement parentElement,
            object context)
        {
            // if current element is not a class then continue walking
            if (element.ElementType != ElementType.Class)
            {
                return true;
            }

            // check whether class name contains "a" letter
            var classElement = (Class)element;
            if (classElement.Declaration.Name.Contains("a"))
            {
                // add violation
                // (note how custom message arguments could be used)
                this.AddViolation(
                    classElement,
                    classElement.Location,
                    RuleNames.SampleRule,
                    classElement.FriendlyTypeText);
            }

            // continue walking in order to find all classes in file
            return true;
        }
    }
}
