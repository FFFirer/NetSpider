using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetSpider.WebApi.Services;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace NetSpider.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CtripController : ControllerBase
    {
        private CtripServices _ctripServices;
        private ILogger _logger;

        public CtripController(CtripServices ctripServices, ILogger<CtripController> logger)
        {
            _ctripServices = ctripServices;
            _logger = logger;
        }

        [HttpGet]
        [Route("plan")]
        public ActionResult GetPlan(string deaprtureTime, string dcity, string acity)
        {
            if (string.IsNullOrEmpty(deaprtureTime))
            {
                return Ok(ResponseResult.SetError("请求参数错误", $"参数{nameof(deaprtureTime)}不能为空"));
            }

            if (string.IsNullOrEmpty(dcity))
            {
                return Ok(ResponseResult.SetError("请求参数错误", $"参数{nameof(dcity)}不能为空"));
            }

            if (string.IsNullOrEmpty(acity))
            {
                return Ok(ResponseResult.SetError("请求参数错误", $"参数{nameof(acity)}不能为空"));
            }

            try
            {
                var data = _ctripServices.GetAllFlightsByPlan(deaprtureTime, dcity, acity);
                return Ok(ResponseResult.SetSuccess(data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"GetPlan:{deaprtureTime},{dcity},{acity}");
                return Ok(ResponseResult.SetError(ex.Message));
            }
        }

        [HttpGet]
        [Route("pricetimeline/{flightNumber}/{departureTime}")]
        public ActionResult GetPriceTimeLine(string flightNumber, string departureTime)
        {
            if (string.IsNullOrEmpty(flightNumber))
            {
                return Ok(ResponseResult.SetError("请求参数错误", $"参数{nameof(flightNumber)}不能为空"));
            }

            if (string.IsNullOrEmpty(departureTime))
            {
                return Ok(ResponseResult.SetError("请求参数错误", $"参数{nameof(departureTime)}不能为空"));
            }

            try
            {
                var data = _ctripServices.GetTimeLineDataByFlightNumber(flightNumber, departureTime);
                return Ok(ResponseResult.SetSuccess(data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"GetPriceTimeLine:{flightNumber}");
                return Ok(ResponseResult.SetError(ex.Message));
            }
        }
    }
}