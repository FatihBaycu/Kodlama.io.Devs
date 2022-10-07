using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProgrammingLanguage : Entity
    {
        public ProgrammingLanguage()
        {
        }

        public string Name { get; set; }

        public ProgrammingLanguage(int Id,string name)
        {
            Id = Id;
            Name = name;
        }
    }
}
