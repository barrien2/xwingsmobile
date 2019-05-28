using System;
using System.Collections.Generic;
using System.Text;
namespace DataAccess.Agents
{
    public class AgentBase<TEntity>: ApiAgentLibStandard.BaseAgent<TEntity> where TEntity: class
    {
        public AgentBase()
        {
            ApiURL = "https://inviktusapi.azurewebsites.net/api/";
            EntityName = typeof(TEntity).Name.ToLower();
        }
    }
}
