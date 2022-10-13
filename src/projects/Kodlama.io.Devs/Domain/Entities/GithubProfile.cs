using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GithubProfile:Entity
    {
        public int UserId { get; set; }
        public string GithubAddress { get; set; }
        public virtual User? User { get; set; }


        public GithubProfile(int Id,int userId, string githubAddress)
        {
            Id = Id;
            UserId = userId;
            GithubAddress = githubAddress;
        }

        public GithubProfile()
        {
        }
    }
}
