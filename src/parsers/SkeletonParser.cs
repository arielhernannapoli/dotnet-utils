using System.Collections.Generic;
using dotnet_utils.src.contracts;
using dotnet_utils.src.enums;
using System.Reflection;
using System.IO;
using System.Linq;

namespace dotnet_utils.src.parsers
{
    public class SkeletonParser : IParser
    {
        public string Parse(skeletontypes skeletonType, IDictionary<string, string> parameters)
        {
            string skeletonContent = string.Empty;

            switch(skeletonType) {
                case skeletontypes.solution:
                    skeletonContent = ParseContent("dotnet_utils.src.skeletons.solution.microservice", parameters);
                    break;
                case skeletontypes.projectAPI:
                    skeletonContent = ParseContent("dotnet_utils.src.skeletons.project.api", parameters);
                    break;                    
                case skeletontypes.projectDomain:
                    skeletonContent = ParseContent("dotnet_utils.src.skeletons.project.domain", parameters);
                    break;
                case skeletontypes.projectInfrastructure:
                    skeletonContent = ParseContent("dotnet_utils.src.skeletons.project.infrastructure", parameters);
                    break;                    
                case skeletontypes.appsettings:
                    skeletonContent = ParseContent("dotnet_utils.src.skeletons.appsettings", parameters);
                    break;                                        
                case skeletontypes.programAPI:
                    skeletonContent = ParseContent("dotnet_utils.src.skeletons.program.api", parameters);
                    break;                                                            
                case skeletontypes.startupAPI:
                    skeletonContent = ParseContent("dotnet_utils.src.skeletons.startup.api", parameters);
                    break;                              
                case skeletontypes.dbcontext:
                    skeletonContent = ParseContent("dotnet_utils.src.skeletons.dbcontext", parameters);
                    break;                                                                                                    
                case skeletontypes.mediatorExtension:
                    skeletonContent = ParseContent("dotnet_utils.src.skeletons.mediator.extension", parameters);
                    break;                                                                                                                        
                case skeletontypes.unitofwork:
                    skeletonContent = ParseContent("dotnet_utils.src.skeletons.unitofwork", parameters);
                    break;                                                                                                                                            
                case skeletontypes.entity:
                    skeletonContent = ParseContent("dotnet_utils.src.skeletons.entity", parameters);
                    break;
                case skeletontypes.homeController:
                    skeletonContent = ParseContent("dotnet_utils.src.skeletons.home.controller", parameters);
                    break;                                                                                                                                                                                    
                case skeletontypes.projectUnitTesting:
                    skeletonContent = ParseContent("dotnet_utils.src.skeletons.project.unit.testing", parameters);
                    break;                                                                                                                                                                                                        
                case skeletontypes.projectIntegrationTesting:
                    skeletonContent = ParseContent("dotnet_utils.src.skeletons.project.integration.testing", parameters);
                    break;                                                                                                                                                                                                                            
                case skeletontypes.crudController:
                    skeletonContent = ParseContent("dotnet_utils.src.skeletons.crud.controller", parameters);
                    break;
                case skeletontypes.aggregate:
                    skeletonContent = ParseContent("dotnet_utils.src.skeletons.aggregate", parameters);
                    break;                    
                case skeletontypes.iAggregateRepository:
                    skeletonContent = ParseContent("dotnet_utils.src.skeletons.i.aggregate.repository", parameters);
                    break;                                        
                case skeletontypes.aggregateRepository:
                    skeletonContent = ParseContent("dotnet_utils.src.skeletons.aggregate.repository", parameters);
                    break;                                                            
                case skeletontypes.iAggregateRoot:
                    skeletonContent = ParseContent("dotnet_utils.src.skeletons.i.aggregate.root", parameters);
                    break;                                        
                case skeletontypes.iRepository:
                    skeletonContent = ParseContent("dotnet_utils.src.skeletons.i.repository", parameters);
                    break;                                                            
                case skeletontypes.entityConfiguration:
                    skeletonContent = ParseContent("dotnet_utils.src.skeletons.entity.configuration", parameters);
                    break;                                                                                
            };

            return skeletonContent;
        }

        private string ParseContent(string resourceName, IDictionary<string, string> parameters)
        {            
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string content = reader.ReadToEnd();
                    if(parameters != null) {
                        parameters.Keys.ToList().ForEach(k => {
                            content = content.Replace(k, parameters[k]);
                        });
                    }
                    return content;
                }
            }
        }
    }
}