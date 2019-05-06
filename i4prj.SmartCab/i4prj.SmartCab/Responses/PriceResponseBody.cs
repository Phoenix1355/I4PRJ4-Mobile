using System;
namespace i4prj.SmartCab.Responses
{
    /// <summary>
    /// Responsebody for PriceResponse
    /// </summary>
    /// <seealso cref="i4prj.SmartCab.Responses.BaseResponseBody" />
    public class PriceResponseBody : BaseResponseBody
    {
        public double price { get; set; }
    }
}
