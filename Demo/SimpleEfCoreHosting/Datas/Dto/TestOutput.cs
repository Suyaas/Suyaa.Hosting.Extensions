using SimpleEfCoreHosting.Entities;
using Suyaa.Hosting.AutoMapper.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEfCoreHosting.Datas.Dto
{
    [MapFrom(typeof(Test))]
    public class TestOutput
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; } = string.Empty;
    }
}
