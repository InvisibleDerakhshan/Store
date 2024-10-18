using System.Collections.Generic;

namespace Bugeto_Store.Application.Services.Products.Queries.GetProductForSite
{
    public partial class ResultProductForSiteDto
    {
        public List<ProductForSiteDto> Products { get; set; }
        public int TotalRow { get; set; }

    }
}
