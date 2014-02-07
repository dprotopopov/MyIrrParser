using System.Web;
using MyParser.Library;

namespace MyIrrParser
{
    public class IrrSearchLookup : WebSearchLookup
    {
        public string Rubric { get; set; }
        public string Keywords { get; set; }
        public int OnlyTitle { get; set; }
        public IrrSearchLookup()
        {
            Url = @"http://irr.ru/searchads{{Rubric}}search/keywords={{Keywords}}/only_title={{OnlyTitle}}/list=list/page{{PageId}}/";
        }
    }
}
