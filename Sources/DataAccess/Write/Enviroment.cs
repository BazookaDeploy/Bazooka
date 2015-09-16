using System.Collections.Generic;

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
    }
}