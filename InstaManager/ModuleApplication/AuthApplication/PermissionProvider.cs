using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace InstaManager.ModuleApplication.AuthApplication;

public class PermissionProvider
{
    public List<ControllerActions> GetAllControllerActions()
    {
        var controllerActions = new List<ControllerActions>();
        
        var assembly = Assembly.GetExecutingAssembly();
        var controllerActionList = assembly.GetTypes()
            .Where(type => typeof(ControllerBase).IsAssignableFrom(type))
            .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
            .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
            .Select(x => new { Controller = x.DeclaringType!.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = string.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })
            .OrderBy(x => x.Controller).ThenBy(x => x.Action)
            .ToList();
        
        foreach (var item in controllerActionList)
        {
            controllerActions.Add(new ControllerActions(){ControllerName = item.Controller,ActionName = item.Action});
        }

        return controllerActions;
    }
}
