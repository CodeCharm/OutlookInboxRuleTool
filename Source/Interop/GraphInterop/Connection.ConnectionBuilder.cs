using System;
using System.Collections.Generic;

using Azure.Core;
using Azure.Identity;

using CodeCharm.Diagnostic;
using CodeCharm.OutlookInterfaces;

using Microsoft.Extensions.DependencyInjection;

namespace CodeCharm.GraphInterop
{
    public partial class Connection
    {
        public class ConnectionBuilder
        {
            private readonly IServiceCollection _services;
            private List<string> _scopes = new List<string>();
            private bool _providerConfigured;
            private InteractiveBrowserCredential _interactiveCredential;
            private TokenCredential _tokenCredential;

            private void AddDistinctScope(string scope)
            {
                if (!_scopes.Contains(scope))
                {
                    _scopes.Add(scope);
                }
            }

            internal ConnectionBuilder()
            {
                _services = new ServiceCollection();
            }

            public IServiceCollection Services => _services;

            public IGraphSession Build()
            {
                if (null != _interactiveCredential)
                {
                    return new Connection(Services, _interactiveCredential, _scopes);
                }
                else if (null != _tokenCredential)
                {
                    return new Connection(Services, _tokenCredential, _scopes);
                }
                else
                {
                    throw new InvalidOperationException("No credential provider");
                }
            }

            public ConnectionBuilder WithFeedback(IFeedback feedback)
            {
                _services.AddSingleton(feedback);
                return this;
            }

            public ConnectionBuilder AddDefaultScopes()
            {
                AddDistinctScope("profile");
                AddDistinctScope("openid");
                AddDistinctScope("email");
                AddDistinctScope("User.Read");
                AddDistinctScope("offline_access");
                AddDistinctScope("Mail.Read");
                AddDistinctScope("MailboxSettings.Read");
                return this;
            }

            public ConnectionBuilder UseBearerAccessTokenProvider()
            {
                if (_providerConfigured)
                {
                    throw new InvalidOperationException("An authentication provider has already been configured");
                }

                _tokenCredential = DelegatedTokenCredential.Create((context, cancellationToken) =>
                {
                    var token = new AccessToken("eyJ0eXAiOiJKV1QiLCJub25jZSI6IkFuWllYNl9oOURCbFpINl91cFg0WVNOQkxfelFVSGJPVDF3dzRDNGExVE0iLCJyaCI6IjAuQWdBQXY0ajVjdkdHcjBHUnF5MTgwQkhiUndNQUFBQUFBQUFBd0FBQUFBQUFBQUFhQUprLiIsImFsZyI6IlJTMjU2IiwieDV0IjoiLUtJM1E5bk5SN2JSb2Z4bWVab1hxYkhaR2V3Iiwia2lkIjoiLUtJM1E5bk5SN2JSb2Z4bWVab1hxYkhaR2V3In0.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTAwMDAtYzAwMC0wMDAwMDAwMDAwMDAiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC83MmY5ODhiZi04NmYxLTQxYWYtOTFhYi0yZDdjZDAxMWRiNDcvIiwiaWF0IjoxNjY5Mjc3OTc3LCJuYmYiOjE2NjkyNzc5NzcsImV4cCI6MTY2OTI4Mjk2MCwiYWNjdCI6MCwiYWNyIjoiMSIsImFjcnMiOlsidXJuOnVzZXI6cmVnaXN0ZXJzZWN1cml0eWluZm8iXSwiYWlvIjoiQVZRQXEvOFRBQUFBeWdLQU5ER2dzTVA2R0dwbDJ6anN4eGNRbk9jSEU1TGp4RlVXSjk0M3pmUHNnc3ljL1JDZU9XbXhxSmJ2Y3NsZnVQVTdZYWJMV0ZEQkhsMlRTaWJrOGJtUjBRUFVXa2JQUWlXMXljeEd4V1E9IiwiYW1yIjpbInJzYSIsIm1mYSJdLCJhcHBfZGlzcGxheW5hbWUiOiJHcmFwaCBFeHBsb3JlciIsImFwcGlkIjoiZGU4YmM4YjUtZDlmOS00OGIxLWE4YWQtYjc0OGRhNzI1MDY0IiwiYXBwaWRhY3IiOiIwIiwiY29udHJvbHMiOlsiYXBwX3JlcyJdLCJjb250cm9sc19hdWRzIjpbImRlOGJjOGI1LWQ5ZjktNDhiMS1hOGFkLWI3NDhkYTcyNTA2NCIsIjAwMDAwMDAzLTAwMDAtMDAwMC1jMDAwLTAwMDAwMDAwMDAwMCIsIjAwMDAwMDAzLTAwMDAtMGZmMS1jZTAwLTAwMDAwMDAwMDAwMCJdLCJkZXZpY2VpZCI6IjRlOGE1ZWQzLTg2NzgtNDA5NS05MThlLTllNTI4YzljMmE3OCIsImZhbWlseV9uYW1lIjoiTWNCZWUiLCJnaXZlbl9uYW1lIjoiQWxhbiIsImlkdHlwIjoidXNlciIsImluX2NvcnAiOiJ0cnVlIiwiaXBhZGRyIjoiOTguMzYuMTU4LjI3IiwibmFtZSI6IkFsYW4gTWNCZWUiLCJvaWQiOiJhYmUwY2NiNi00ZWNiLTRjOTEtYWE4NC1jNWVkMjE3OWMyNjciLCJvbnByZW1fc2lkIjoiUy0xLTUtMjEtMTI0NTI1MDk1LTcwODI1OTYzNy0xNTQzMTE5MDIxLTE4MDg3MTEiLCJwbGF0ZiI6IjMiLCJwdWlkIjoiMTAwMzAwMDBBOEI4NjQ1MiIsInJoIjoiSSIsInNjcCI6IkNhbGVuZGFycy5SZWFkV3JpdGUgQ29udGFjdHMuUmVhZFdyaXRlIERldmljZU1hbmFnZW1lbnRBcHBzLlJlYWRXcml0ZS5BbGwgRGV2aWNlTWFuYWdlbWVudENvbmZpZ3VyYXRpb24uUmVhZC5BbGwgRGV2aWNlTWFuYWdlbWVudENvbmZpZ3VyYXRpb24uUmVhZFdyaXRlLkFsbCBEZXZpY2VNYW5hZ2VtZW50TWFuYWdlZERldmljZXMuUHJpdmlsZWdlZE9wZXJhdGlvbnMuQWxsIERldmljZU1hbmFnZW1lbnRNYW5hZ2VkRGV2aWNlcy5SZWFkLkFsbCBEZXZpY2VNYW5hZ2VtZW50TWFuYWdlZERldmljZXMuUmVhZFdyaXRlLkFsbCBEZXZpY2VNYW5hZ2VtZW50UkJBQy5SZWFkLkFsbCBEZXZpY2VNYW5hZ2VtZW50UkJBQy5SZWFkV3JpdGUuQWxsIERldmljZU1hbmFnZW1lbnRTZXJ2aWNlQ29uZmlnLlJlYWQuQWxsIERldmljZU1hbmFnZW1lbnRTZXJ2aWNlQ29uZmlnLlJlYWRXcml0ZS5BbGwgRGlyZWN0b3J5LkFjY2Vzc0FzVXNlci5BbGwgRGlyZWN0b3J5LlJlYWRXcml0ZS5BbGwgRmlsZXMuUmVhZFdyaXRlLkFsbCBHcm91cC5SZWFkV3JpdGUuQWxsIElkZW50aXR5Umlza0V2ZW50LlJlYWQuQWxsIE1haWwuUmVhZFdyaXRlIE1haWxib3hTZXR0aW5ncy5SZWFkV3JpdGUgTm90ZXMuUmVhZFdyaXRlLkFsbCBvcGVuaWQgUGVvcGxlLlJlYWQgUHJlc2VuY2UuUmVhZCBQcmVzZW5jZS5SZWFkLkFsbCBwcm9maWxlIFJlcG9ydHMuUmVhZC5BbGwgU2l0ZXMuUmVhZFdyaXRlLkFsbCBUYXNrcy5SZWFkV3JpdGUgVXNlci5SZWFkIFVzZXIuUmVhZEJhc2ljLkFsbCBVc2VyLlJlYWRXcml0ZSBVc2VyLlJlYWRXcml0ZS5BbGwgZW1haWwiLCJzaWduaW5fc3RhdGUiOlsiZHZjX21uZ2QiLCJkdmNfY21wIiwia21zaSJdLCJzdWIiOiJ3T2I1NWV1azRJYk9qRS15encwYWJzQ3FfYV9helMxZVB5ZHE5bkdmYnFvIiwidGVuYW50X3JlZ2lvbl9zY29wZSI6IldXIiwidGlkIjoiNzJmOTg4YmYtODZmMS00MWFmLTkxYWItMmQ3Y2QwMTFkYjQ3IiwidW5pcXVlX25hbWUiOiJ3aW1jYmVlQG1pY3Jvc29mdC5jb20iLCJ1cG4iOiJ3aW1jYmVlQG1pY3Jvc29mdC5jb20iLCJ1dGkiOiJzR0pUUXVtSHJVQ19kSU1CSnlnTEFBIiwidmVyIjoiMS4wIiwid2lkcyI6WyJiNzlmYmY0ZC0zZWY5LTQ2ODktODE0My03NmIxOTRlODU1MDkiXSwieG1zX3N0Ijp7InN1YiI6InQ1Y3U1ZGtQLWdnRENrZFAwZWkzemRtZHhoVWtrRUFEdk9lWUtJZHpTeVUifSwieG1zX3RjZHQiOjEyODkyNDE1NDd9.gbvVAbNuiISgkYFMLOOjN4LdaYmlnZHF4Z0Gm8xa4GPvk0K_205x74wyXCy9k250oNSNO2xsiYX2_DlXkbZjaofmaTcLs5DVeYGcv-GYp6VpBu0fsuBAxDekyMUNpFwrE0jpED8uQx8l3AN4bEpXZgot_7VxWbAMQVTAHPTZrNgfnFssc3tbg35p2rilkXwpaxSGjsAADErti3rDDkB4ir_JzHVu-P23HqQtcb2zcmQt-lyyyokuUQ5N_L4lHjp1Vj31UL6UEIY6R-Am8dH62s7fVqwEKToQnfMUPTGsaxJly5-aKMpfVzkatowT2uTp2F2D3uaxgn-LsY8XViU8mQ", DateTimeOffset.Now.AddHours(1));
                    return token;
                });

                _providerConfigured = true;

                return this;
            }

            public ConnectionBuilder UseInteractiveAuthenticationProvider()
            {
                if (_providerConfigured)
                {
                    throw new InvalidOperationException("An authentication provider has already been configured");
                }

                //var tenantId = "common";
                var tenantId = "72f988bf-86f1-41af-91ab-2d7cd011db47";
                var clientId = "6cd7ead2-8627-4055-a4e1-a6167b13d25a";

                // using Azure.Identity;
                var options = new InteractiveBrowserCredentialOptions
                {
                    TenantId = tenantId,
                    ClientId = clientId,
                    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
                    // MUST be http://localhost or http://localhost:PORT
                    // See https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/wiki/System-Browser-on-.Net-Core
                    RedirectUri = new Uri("http://localhost:57441"),
                    //RedirectUri = new Uri("https://login.microsoftonline.com/common/oauth2/nativeclient")
                };

                // https://learn.microsoft.com/dotnet/api/azure.identity.interactivebrowsercredential
                _interactiveCredential = new InteractiveBrowserCredential(options);
                _providerConfigured = true;

                return this;
            }
        }

    }
}