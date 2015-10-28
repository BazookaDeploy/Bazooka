using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Write
{
    public class Enviroment
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual IList<Agent> Agents { get; set; }

        public virtual void AddAgent(string name, string address)
        {
            this.Agents.Add(new Agent()
            {
                EnviromentId = this.Id,
                Name = name,
                Address = address
            });
        }

        public virtual void ChangeAgentAddress(int agentId, string newAddress)
        {
            this.Agents.Single(x => x.Id == agentId).Address = newAddress;
        }

        public virtual void RenameAgent(int agentId, string name)
        {
            this.Agents.Single(x => x.Id == agentId).Name = name;
        }
    }
}