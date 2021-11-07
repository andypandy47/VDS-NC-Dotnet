using System.Collections.Generic;
using Newtonsoft.Json;

namespace VDS_NC_Dotnet
{
    public class ProofOfVaccination
    {
        /// <summary>
        /// Unique Vaccination Certificate Identifier
        /// Max size: 12
        /// </summary>
        [JsonProperty("uvci")]
        public string UVCI { get; set; }

        [JsonProperty("pid")]
        public PersonalIdentification PersonalIdentification { get; set; }

        [JsonProperty("ve")]
        public IEnumerable<VaccinationEvent> VaccinationEvents { get; set; }
    }

    public class PersonalIdentification
    {
        /// <summary>
        /// Name of the holder
        /// Max size: 39
        /// </summary>
        [JsonProperty("n")]
        public string Name { get; set; }

        /// <summary>
        /// Date of birth of test subject. ISO8601 YYYY-MM-DD
        /// Max size: 10
        /// </summary>
        [JsonProperty("dob", NullValueHandling = NullValueHandling.Ignore)]
        public string DateOfBirth { get; set; }

        /// <summary>
        /// Travel Document Number
        /// Single Unique Identifier only. Identifier should be valid Travel Document number
        /// Max size: 11
        /// </summary>
        [JsonProperty("i", NullValueHandling = NullValueHandling.Ignore)]
        public string UniqueIdentifier { get; set; }

        /// <summary>
        /// Any other document number at discretion of issuer
        /// Max size: 24
        /// </summary>
        [JsonProperty("ai", NullValueHandling = NullValueHandling.Ignore)]
        public string AdditionalIdentifier { get; set; }

        /// <summary>
        /// Sex of the test subject (as specified in Doc 9303-4 Section 4.1.1.1 – Visual Inspection Zone)
        /// Max size: 1
        /// </summary>
        [JsonProperty("sex", NullValueHandling = NullValueHandling.Ignore)]
        public string Sex { get; set; }
    }

    public class VaccinationEvent
    {
        /// <summary>
        /// Vaccine or vaccine sub-type (ICD-11 Extension codes (http://id.who.int/icd/entity/164949870)
        /// Max size: 6
        /// </summary>
        [JsonProperty("des")]
        public string VaccineProhylaxis { get; set; }

        /// <summary>
        /// Medicinal product name
        /// </summary>
        [JsonProperty("nam")]
        public string VaccineBrand { get; set; }

        /// <summary>
        /// Disease or agent that the vaccination provides protection against (ICD-11)
        /// Max size: 6
        /// </summary>
        [JsonProperty("dis", NullValueHandling = NullValueHandling.Ignore)]
        public string DiseaseOrAgentTargeted { get; set; }

        [JsonProperty("vd")]
        public IEnumerable<VaccinationDetails> VaccinationDetails { get; set; }
    }

    public class VaccinationDetails
    {
        /// <summary>
        /// Date on which the vaccine was administered. The ISO8601 full
        ///date format YYYY-MM-DD MUST be used.
        ///Max size: 10
        /// </summary>
        [JsonProperty("dvc")]
        public string DateOfVaccination { get; set; }

        /// <summary>
        /// Vaccine dose number.
        /// Max size: 2
        /// </summary>
        [JsonProperty("seq")]
        public int DoseNumber { get; set; }

        /// <summary>
        /// The country in which the individual has been vaccinated.
        ///A three letter code identifying the issuing state or organization.
        ///The three letter code is according to Doc 9303-3.
        ///Max size: 3
        /// </summary>
        [JsonProperty("ctr")]
        public string CountryOfVaccination { get; set; }

        /// <summary>
        /// The name or identifier of the vaccination facility responsible for providing the vaccination 
        /// Max size: 20
        /// </summary>
        [JsonProperty("adm")]
        public string AdminsteringCentre { get; set; }

        /// <summary>
        /// A distinctive combination of numbers and/or letters which specifically identifies a batch
        /// Max size: 20
        /// </summary>
        [JsonProperty("lot")]
        public string VaccineBatchNumber { get; set; }

        /// <summary>
        /// Date on which the next vaccination should be administered. The ISO8601 full
        ///date format YYYY-MM-DD MUST be used.
        ///Max size: 10
        /// </summary>
        [JsonProperty("dvn", NullValueHandling = NullValueHandling.Ignore)]
        public string DateofNextDose { get; set; }
    }
}
