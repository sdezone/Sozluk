﻿using System;
namespace Sozluk.Api.Domain.Models
{
	public abstract class BaseEntity
	{
		public Guid Id { get; set; }
		public DateTime CreateDate { get; set; }
	}
}

