using System;
using System.Collections.Generic;

namespace WebApi.SBS.Core.Swagger
{
    public class SwaggerConfiguration
    {
        public string Title { get; set; }
        public string JsonRoute { get; set; }
        public string Description { get; set; }
        public List<Version> Versions { get; set; }

        public class Version
        {
            public string Name { get; set; }
            public string UiEndpoint { get; set; }
        }
    }

}
