using System;

namespace buckstore.auth.service.api.v1.Dtos
{
    public class BaseResponseDto<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
    }
}
