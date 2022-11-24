using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Graph.Beta.Models;

namespace CodeCharm.OutlookInterfaces
{
    public interface IGraphSession
        : IConnection
    {
        Task<MessageRuleCollectionResponse> GetInboxRules();
        Task<User> GetMeAsync();
    }
}
