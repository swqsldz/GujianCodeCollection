﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GujianCodeCollection.Entity
{

    public class CardInfo
    {

        [JsonProperty("card_number")]
        public string CardNumber { get; set; }

        [JsonProperty("card_pwd")]
        public string CardPwd { get; set; }
    }

    public class Code_17173
    {

        [JsonProperty("flag")]
        public int Flag { get; set; }

        [JsonProperty("cardInfo")]
        public CardInfo CardInfo { get; set; }
    }

}
