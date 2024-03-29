﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Luciferin.DataLayer.Storage.Entities
{
    public class Setting
    {
        #region Properties

        public string Category { get; set; }
        
        public bool? BooleanValue { get; set; }

        public int? IntValue { get; set; }

        [Column(Order = 0)]
        [Key]
        [StringLength(250)]
        public string Key { get; set; }

        public string StringValue { get; set; } = "";

        public TimeSpan? TimeSpanValue { get; set; }

        [Column(Order = 1)]
        [StringLength(10)]
        public string ValueType { get; set; }

        #endregion
    }
}