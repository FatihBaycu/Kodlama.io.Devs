using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Dtos
{
    public class UserOperationClaimListDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string OperationClaimId { get; set; }
        public string OperationClaimName { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserEmail { get; set; }


    }
}
