﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.CAPP.Model
{
    /// <summary>
    /// 类型说明：基础资源库
    /// 作      者：jason.tang
    /// 完成时间：2013-08-23
    /// </summary>
    public class BaseResource
    {
        /// <summary>
        /// 资源ID
        /// </summary>
        public string ResourceId { get; set; }
        /// <summary>
        /// 字段代码
        /// </summary>
        public string FieldCode { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 父字段
        /// </summary>
        public string ParentField { get; set; }
        /// <summary>
        /// 子字段和
        /// </summary>
        public int ChildCount { get; set; }
    }
}
