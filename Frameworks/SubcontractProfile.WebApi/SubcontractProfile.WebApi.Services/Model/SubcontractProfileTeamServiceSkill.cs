using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.WebApi.Services.Model
{
   public class SubcontractProfileTeamServiceSkill
    {
        public System.Guid TeamId { get; set; }
        public string Skill_Id { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
