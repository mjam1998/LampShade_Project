﻿

namespace _0_Freamwork.Infrastructure
{
    public class PermissionDto
    {
        public int Code { get; set; }
        public string Name { get; set; }

        public PermissionDto(int code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
