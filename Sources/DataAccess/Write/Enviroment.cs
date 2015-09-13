using System.Collections.Generic;

namespace DataAccess.Write
{
    public class Enviroment
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual IList<Agent> Agents { get; set; }

        public void AddAgent(string name, string address)
        {
            this.Agents.Add(new Agent()
            {
                Name = name,
                Address = address
            });
        }
    }
}