﻿using static Sozluk.Common.ViewModels.Enums;

namespace Sozluk.Api.Domain.Models
{
    public class EntryVote:BaseEntity
    {
        public Guid EntryId { get; set; }
        public VoteType VoteType { get; set; }
        public Guid CreatedById { get; set; }
        public virtual Entry Entry { get; set; }
    }
}