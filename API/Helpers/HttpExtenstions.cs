using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Helpers
{
    public static class HttpExtenstions
    {
        public static void AddPagenationHeader(this HttpResponse response, int currentPage, int itemsPerPage,
            int totalItems, int totalPages)
        {
            var pagenationHeader = new PagenationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            response.Headers.Add("Pagenation", JsonSerializer.Serialize(pagenationHeader));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagenation");
        }
    }
}