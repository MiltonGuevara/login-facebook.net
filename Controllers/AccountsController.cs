﻿namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;
using WebApi.Models.Accounts;
using WebApi.Services;

[ApiController]
[Authorize]
[Route("[controller]")]
public class AccountsController : BaseController
{
    private IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest model)
    {
        var response = await _accountService.Authenticate(model);
        return Ok(response);
    }

    [HttpGet("current")]
    public IActionResult GetCurrent()
    {
        return Ok(Account);
    }

    [HttpPut("current")]
    public async Task<IActionResult> UpdateCurrent(UpdateRequest model)
    {
        var account = await _accountService.Update(Account!.Id, model);
        return Ok(account);
    }

    [HttpDelete("current")]
    public async Task<IActionResult> DeleteCurrent()
    {
        await _accountService.Delete(Account!.Id);
        return Ok();
    }
}
