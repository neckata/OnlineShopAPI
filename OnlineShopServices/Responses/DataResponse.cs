﻿namespace OnlineShopServices.Service.Response
{
    /// <summary>
    /// response class for data returned method
    /// </summary>
    public class DataResponse<T> : Response
    {
        public T Data { get; set; }
    }
}
