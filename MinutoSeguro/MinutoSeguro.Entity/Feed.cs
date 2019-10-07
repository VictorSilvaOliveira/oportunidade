using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace MinutoSeguro.Entity
{
    public class Feed
    {
        public Feed()
        {
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public ReadOnlyCollection<string> Category { get; set; }

    }
}