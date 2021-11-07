using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace VDS_NC_Dotnet
{
    public class VDS_NC
    {
        [JsonProperty("data")]
        public VDS_NC_Data Data { get; set; }

        [JsonProperty("sig")]
        public SignatureZone SignatureZone { get; set; }
    }

    public class VDS_NC_Data
    {
        [JsonProperty("hdr")]
        public HeaderZone Header { get; set; }
    }

    public class VDS_NC_POV : VDS_NC_Data
    {
        [JsonProperty("msg")]
        public ProofOfVaccination Message { get; set; }
    }

    public class VDS_NC_POT : VDS_NC_Data
    {
        [JsonProperty("msg")]
        public ProofOfTest Message { get; set; }
    }

    public class HeaderZone
    {
        /// <summary>
        /// Type is set to “icao.test” for PoT (data defined by CAPSCA), “icao.vacc” for PoV (data defined by WHO). Other Types may be 
        ///added in the future 
        /// </summary>
        [JsonProperty("t")]
        public string Type { get; set; }

        /// <summary>
        /// Each of the use cases will define a version number for the structure. In case of changes in structure, the version number will
        /// be incremented. 
        /// </summary>
        [JsonProperty("v")]
        public int Version { get; set; }

        /// <summary>
        /// A three letter code identifying the issuing state or organization. The three letter code is according to Doc 9303-3.
        /// </summary>
        [JsonProperty("is")]
        public string IssuingCountry { get; set; }
    }

    public class SignatureZone
    {
        /// <summary>
        /// The signature algorithm used to produce the signature. Signatures MUST be ECDSA. A key length of 256 bit in combination with
        ///SHA-256(at the time this document is created) is RECOMMENDED. 
        /// </summary>
        [JsonProperty("alg")]
        public string Algorithm { get; set; }

        /// <summary>
        /// X.509 signer certificate in base64url [RFC 4648] 
        /// </summary>
        [JsonProperty("cer")]
        public string Certificate { get; set; }

        /// <summary>
        /// Signature value signed over the Data in base64url [RFC 4648] 
        /// </summary>
        [JsonProperty("sigvl")]
        public string SignatureValue { get; set; }
    }
}
