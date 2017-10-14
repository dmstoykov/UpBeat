using System.Web;

namespace UpBeat.Common.Constants
{
    public class Resources
    {
        public static readonly string DbSeedPath = HttpContext.Current.Server.MapPath("~/App_Data/dbSeed.json");

        public const string DefaultAlbumImageUrl = "https://d2qqvwdwi4u972.cloudfront.net/static/img/default_album.png";
    }
}
