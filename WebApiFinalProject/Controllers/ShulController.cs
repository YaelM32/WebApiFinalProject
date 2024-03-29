﻿using AutoMapper;
using BusinessLogic.Dto;
using BusinessLogic.DTO;
using BusinessLogic.IService;
using DataAccess.DBModels;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class ShulController : ControllerBase
    {
        IShulService shulService;
        IMapper mapper;
        public ShulController(IShulService _shulService, IMapper _mapper)
        {
            shulService = _shulService;
            mapper = _mapper;
        }
        [HttpGet("GetShuls")]
        [AllowAnonymous]
        public async Task<List<ShulDTO>> GetShuls()
        {
            List<Shul> l = await shulService.GetShuls();
            return mapper.Map<List<ShulDTO>>(l);
        }
        [HttpPost("SignIn")]
        //הירשמות פעם ראשונה למערכת
        [AllowAnonymous]
        public Task<int> SignIn([FromBody] ShulDTO shulDTO)
        {
            Shul shul = mapper.Map<Shul>(shulDTO);
            return shulService.SignIn(shul);
        }
        [HttpGet("GetShulById")]
        [AllowAnonymous]
        //קבלת כל פרטי בית כנסת מסוים
        public async Task<ShulDTO> GetShulById([FromQuery] int shulId)
        {
            Shul s = await shulService.GetShulById(shulId);
            return mapper.Map<ShulDTO>(s);
        }

        [HttpPost, Route("UploadImage")]
        [AllowAnonymous]
        public async Task UploadFile(int shulId, IFormFile userfile)
        {

            try
            {
                shulService.UploadFile(shulId, userfile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                await SaveFileName(userfile);
                SetMap(shulId, userfile.FileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        [HttpGet, Route("SaveFileName")]
        public async Task<string> SaveFileName(IFormFile userfile)
        {
            string filename = userfile.FileName;
            filename = Path.GetFileName(filename);
            string uploadFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", filename);
            await using var stream = new FileStream(uploadFilePath, FileMode.Create);
            await userfile.CopyToAsync(stream);
            return filename;
        }
        [HttpPost, Route("UploadLogo")]
        public async Task UploadLogo(int shulId, IFormFile logo)
        {
            try
            {
                await shulService.UploadFile(shulId, logo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                string name = await SaveFileName(logo);
                await SetLogo(shulId, logo.FileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        [HttpPut("SetMap")]

        public Task SetMap(int shulId, string Filename)
        {
            return shulService.SetMap(shulId, Filename);
        }

        [HttpPut("SetLogo")]
        public Task SetLogo(int shulId, string Filename)
        {
            return shulService.SetLogo(shulId, Filename);
        }

        [HttpPut("EditShulDetails")]
        public async Task EditShulDetails([FromQuery] ShulDTO2 shulDTO)//, IFormFile img)
        {
            if(shulDTO.Logo!=null)
               await UploadLogo(shulDTO.Id, shulDTO.Logo);
            Shul shul = mapper.Map<Shul>(shulDTO);
            await shulService.EditShulDetails(shulDTO.Id, shul);
        }

    }
}




