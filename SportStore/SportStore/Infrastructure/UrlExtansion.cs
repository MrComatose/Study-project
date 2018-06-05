using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SportStore.Infrastructure
{
    public static class UrlExtansion
    {
        public static string PathAndQuery(this HttpRequest request) =>
          request.QueryString.HasValue
              ? $"{request.Path}{request.QueryString}"
              : request.Path.ToString();
    }
}
