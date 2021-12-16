using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Yitter.IdGenerator;

namespace SnowIdGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// 获取 SnowId
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getSnowId")]
        public long GetSnowId()
        {
            var result = YitIdHelper.NextId();
            Console.WriteLine(result.ToString().Length);
            return result;
        }

    }
}
