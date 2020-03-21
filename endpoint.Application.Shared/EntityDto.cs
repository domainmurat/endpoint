using System;
using System.Collections.Generic;
using System.Text;

namespace endpoint.Application.Shared
{
    public class EntityDto<T>
    {
        public T Id { get; set; }
    }

    public class EntityDto : EntityDto<int>
    {
    }
}
