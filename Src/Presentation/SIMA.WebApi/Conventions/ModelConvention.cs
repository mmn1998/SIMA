using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Text.RegularExpressions;

namespace SIMA.WebApi.Conventions
{
    public class ModelConvention : IApplicationModelConvention
    {
        private const string Query = "QueryController";
        private const string StrRegex = @"api/v\d+/";
        private const string serviceCatalogPattern = @"(serviceCatalog/)|(basic/)|(bcp/)|(riskManagement/)|(trusty/)|(assetAndConfiguration/)|(branch/)";
        public void Apply(ApplicationModel application)
        {
            //foreach (var controller in application.Controllers.Where(a => a.ControllerType.Name.EndsWith(Query, StringComparison.OrdinalIgnoreCase)))
            //{
            //    foreach (var selectorModel in controller.Selectors.Where(x => x.AttributeRouteModel != null).ToList())
            //    {
            //        var existingTemplate = selectorModel.AttributeRouteModel.Template;
            //        var match = Regex.Match(existingTemplate, StrRegex, RegexOptions.IgnoreCase);

            //        string newTemplate = existingTemplate;

            //        if (match.Success && !string.IsNullOrEmpty(match.Value))
            //        {
            //            newTemplate = newTemplate.Replace(match.Value, "");
            //        }

            //        newTemplate = newTemplate.Replace(Query, "", StringComparison.OrdinalIgnoreCase);

            //        selectorModel.AttributeRouteModel = new AttributeRouteModel
            //        {
            //            Template = newTemplate
            //        };
            //    }
            //}
            /// TODO : Should modify
            foreach (var controller in application.Controllers.Where(a => a.ControllerType.Name.EndsWith(Query, StringComparison.OrdinalIgnoreCase)))
            {
                foreach (var selectorModel in controller.Selectors.Where(x => x.AttributeRouteModel != null).ToList())
                {
                    var newTemplate = string.Empty;
                    var match = Regex.Match(selectorModel.AttributeRouteModel.Template, StrRegex, RegexOptions.IgnoreCase);
                    var serviceCatalogMatch = Regex.Match(selectorModel.AttributeRouteModel.Template, serviceCatalogPattern, RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        newTemplate = match.Value;
                    }
                    if (serviceCatalogMatch.Success)
                    {
                        newTemplate = serviceCatalogMatch.Value;
                    }

                    selectorModel.AttributeRouteModel =
                        new AttributeRouteModel
                        {
                            Template = newTemplate +
                                       controller.ControllerType.Name.Replace(Query, "", StringComparison.OrdinalIgnoreCase)
                        };
                }
            }
        }
    }
}
