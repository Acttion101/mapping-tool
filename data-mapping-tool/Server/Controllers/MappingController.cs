using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JUST;
using datamappingtool.Server.Models;
using System.Text.Json;
using System.Text.RegularExpressions;


namespace datamappingtool.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
  
        public class MappingController : Controller
        {
            [HttpPost("tranform")]
            public IActionResult Index(MappingModel mappingModel)
            {

         
                string transformedString;
                try
                {
                    if (string.IsNullOrEmpty(mappingModel.Input) || string.IsNullOrEmpty(mappingModel.Output))
                    {
                        var transform = JsonSerializer.Serialize(mappingModel.Output);
                        var source = JsonSerializer.Serialize(mappingModel.Input);
                        transformedString = JsonTransformer.Transform(transform, source);
                    }
                    else
                    {
                        var input = Regex.Replace(mappingModel.Input, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");
                        var output = Regex.Replace(mappingModel.Output, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");
                        transformedString = JsonTransformer.Transform(output, input);
                    }
                    return Ok(transformedString);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        
    }


