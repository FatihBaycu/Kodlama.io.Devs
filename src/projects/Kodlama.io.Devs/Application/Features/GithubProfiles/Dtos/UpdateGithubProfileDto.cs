using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfiles.Dtos
{
    public class UpdateGithubProfileDto
    {
        public int Id { get; set; }  
        public int UserId { get; set; }  
        public string GithubAddress { get; set; }  
    }
}
