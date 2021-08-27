﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWallWebAPI.Domain.Models;
using MyWallWebAPI.Domain.Models.DTOs;
using MyWallWebAPI.Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWallWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("list-users")]
        public async Task<ActionResult> ListUsers()
        {
            try
            {
                ApplicationUser currentUser = await _authService.GetCurrentUser();
                List<ApplicationUser> list = await _authService.ListUsers(currentUser.Id);

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("sign-up")]
        public async Task<ActionResult> SignUp([FromBody] SignUpDTO signUpDTO)
        {
            try
            {
                bool ret = await _authService.SignUp(signUpDTO);

                return Ok(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<ActionResult> SignIn([FromBody] SignInDTO signInDTO)
        {
            try
            {
                SsoDTO ssoDTO = await _authService.SignIn(signInDTO);

                return Ok(ssoDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-current-user")]
        public async Task<ActionResult> GetCurrentUser()
        {
            try
            {
                ApplicationUser currentUser = await _authService.GetCurrentUser();

                return Ok(currentUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-user-by-username")]
        public async Task<ActionResult> GetUserByUserName([FromQuery] string userName)
        {
            try
            {
                ApplicationUser currentUser = await _authService.GetUserByUserName(userName);

                return Ok(currentUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}