using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpBeat.Data.Models;

namespace UpBeat.Common.Constants
{
    public class Resources
    {
        public const string DbSeedPath = "D:\\Telerik Academy\\Teamwork Projects\\AspNetTeamwork\\UpBeat\\Resources\\dbSeed.json";

        public const string DefaultAlbumImageUrl = "https://d2qqvwdwi4u972.cloudfront.net/static/img/default_album.png";

        public static readonly Image DefaultAlbumImage = new Image() { Width = 300, Height = 300, Url = "https://d2qqvwdwi4u972.cloudfront.net/static/img/default_album.png" };
    }
}
