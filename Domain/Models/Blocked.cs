﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    public class Blocked
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [Required(ErrorMessage = "Cnpj é um campo obrigatório")]
        [StringLength(19)]
        public string Cnpj { get; set; } //mascara
        [Required(ErrorMessage = "Nome é um campo obrigatório")]
        [StringLength(30)]
        public string Name { get; set; }
        [StringLength(30)]
        public string NameOpt { get; set; }
        public DateTime DtOpen { get; set; }
        public bool Status { get; set; }

        public Address Adress { get; set; }

        public Aircraft Aircraft { get; set; }

    }
}
