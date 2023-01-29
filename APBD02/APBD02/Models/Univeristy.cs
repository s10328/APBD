using APBD02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD02.Models
{
    internal class University
    {
        public List<Student> studenci { get; set; }
        public List<Studies> kierunki { get; set; }

    }
}
