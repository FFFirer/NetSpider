using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace NetSpider.Domain.Ctrip
{
    public class CtripMapper : Profile
    {
        public CtripMapper()
        {
            // 航班Summary
            CreateMap<Flight, FlightSummaryDto>()
                .BeforeMap((source, dto) =>
                {
                    dto.CreatedTime = new DateTime(source.TimeTicks);
                });

            CreateMap<FlightSummaryDto, Flight>()
                .BeforeMap((dto, source) =>
                {
                    source.TimeTicks = dto.CreatedTime.Ticks;
                });

            // 航班TimeLine

        }
    }
}
