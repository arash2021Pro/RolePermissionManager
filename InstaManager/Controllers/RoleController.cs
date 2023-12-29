using CoreBussiness.BussinessEntity.Roles;
using CoreBussiness.DTO.Permissions;
using CoreBussiness.DTO.Roles;
using CoreBussiness.DTO.Users;
using InstaManager.ModuleApplication.AuthApplication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Segerations.Permissions.Command;
using Segerations.Roles.Command;
using Segerations.Roles.Query;
using Segerations.Users.Command;

namespace InstaManager.Controllers;

[ApiController]
[Route("/[controller]")]
public class RoleController:ControllerBase
{
    private IMediator _mediator;
    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// creating roles for users . roleId is not required to be sent , this is for edit mode
    /// </summary>
    /// <param name="roleModel"></param>
    /// <returns></returns>
    [HttpPost]
    [TokenValidator]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> CreateRole([FromBody]RoleModel roleModel)
    {
        var result = await _mediator.Send(new CreateRoleCommand() { RoleModel = roleModel });
        return result.Success ? Ok(result.Data) : BadRequest(result.Error);
    }

    /// <summary>
    /// this is editing created role
    /// </summary>
    /// <param name="roleModel"></param>
    /// <returns></returns>
    [HttpPut]
    [TokenValidator]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> EditRole([FromBody] RoleModel roleModel)
    {
        var result = await _mediator.Send(new EditRoleCommand() {RoleModel = roleModel});
        return result.Success ? Ok(result.Data) : BadRequest(result.Error);
    }
    /// <summary>
    /// changing status for roles
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    [HttpPut]
    [TokenValidator]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> ChangeRoleStatus([FromBody] int roleId)
    {
        var result = await _mediator.Send(new EditRoleStatusCommand() {RoleId = roleId});
        return result.Success ? Ok(result.Data) : BadRequest(result.Error);
    }
    /// <summary>
    /// deleting roles
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    [HttpDelete]
    [TokenValidator]
    [Route("/[controller]/[action]/{roleId}")]
    public async Task<IActionResult> DeleteRole([FromRoute] int roleId)
    {
        var result = await _mediator.Send(new DeleteRoleCommand() {RoleId = roleId});
        return result.Success ? Ok(result.Data) : BadRequest(result.Error);
    }
    /// <summary>
    /// listing all roles
    /// </summary>
    /// <param name="roleQuery"></param>
    /// <returns></returns>
    [HttpGet]
    [TokenValidator]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> GetAllRoles([FromQuery] RoleQueryFilter roleQuery)
    {
        var result = await _mediator.Send(new ListRoleQuery() {RoleQueryFilter = roleQuery});
        return result.Success ? Ok(result.Data) : BadRequest(result.Error);
    }
    /// <summary>
    /// get an specific role via Id
    /// </summary>
    /// <param name="roleQuery"></param>
    /// <returns></returns>
    [HttpGet]
    [TokenValidator]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> GetRole([FromQuery] GetRoleQuery roleQuery)
    {
        var result = await _mediator.Send(new GetRoleQuery() {RoleId = roleQuery.RoleId});
        return result.Success ? Ok(result.Data) : BadRequest(result.Error);
    }
    
    /// <summary>
    /// listing all permissions to be set for a role
    /// </summary>
    /// <returns></returns>
    [HttpGet] 
    [TokenValidator] 
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> GetAllPermissions()
    {
        var permissionProvider = new PermissionProvider();
        var permissions =  permissionProvider.GetAllControllerActions();
        return Ok(permissions);
    }
    
    /// <summary>
    /// setting permissions for role
    /// </summary>
    /// <param name="rolePermissions"></param>
    /// <returns></returns>
    [HttpPost]
    [TokenValidator]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> SetRolePermission([FromBody] RolePermissions rolePermissions)
    {
        var result = await _mediator.Send(new SetRolePermissionCommand() {RolePermissions = rolePermissions});
        return result.Success ? Ok(result.Data) : BadRequest(result.Error);
    }

    /// <summary>
    /// setting roles for users
    /// </summary>
    /// <param name="editUserRole"></param>
    /// <returns></returns>
    [HttpPut]
    [TokenValidator]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> SetUserRole([FromBody] EditUserRole editUserRole)
    {
        var result = await _mediator.Send(new EditUserRoleCommand(){EditUserRole = editUserRole});
        return result.Success ? Ok(result.Data) : BadRequest(result.Error);
    }

    /// <summary>
    /// changing status for permissions
    /// </summary>
    /// <param name="permissionChangeStatus"></param>
    /// <returns></returns>
    [HttpPut]
    [TokenValidator]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> ChangePermissionStatus([FromBody] PermissionChangeStatus permissionChangeStatus)
    {
        var result = await _mediator.Send(new EditPermissionStatusCommand(){PermissionChangeStatus = permissionChangeStatus});
        return result.Success ? Ok(result.Data) : BadRequest(result.Error);
    }

 
    
}