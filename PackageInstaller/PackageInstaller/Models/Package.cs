using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageInstaller.Models
{
    public class Package
    {
        public string Name { get; set; }
        public string Dependency { get; set; }
    }
}
