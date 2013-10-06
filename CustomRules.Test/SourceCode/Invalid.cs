namespace CustomRules.Test.SourceCode
{
    using System.Web;

    public class Invalid
    {
        private string url = HttpContext.Current.Request.RawUrl;
    }
}
