using System;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json;

namespace VDS_NC_Dotnet
{
    /// @author Andrew Kay()
    /// <summary>
    /// Service exposing functions for signing and verification of an ICAO VDS-NC data
    /// </summary>
    public class VDS_NC_Service
    {
        /// <summary>
        /// Constructor that accepts an optional hash algorithm
        /// </summary>
        /// <param name="hashAlgorithm">Defaults to reccomended value SHA256</param>
        public VDS_NC_Service(HashAlgorithmName? hashAlgorithm = null)
        {
            this.HashAlgorithm = hashAlgorithm ?? HashAlgorithmName.SHA256;

            if (!this.SupportedAlgorithms.Contains(this.HashAlgorithm))
            {
                throw new Exception($"Invalid hash algorithm: {this.HashAlgorithm} is not supported by VDS-NC");
            }
        }

        private HashAlgorithmName[] SupportedAlgorithms { get; } = { HashAlgorithmName.SHA256, HashAlgorithmName.SHA384, HashAlgorithmName.SHA512 };

        private HashAlgorithmName HashAlgorithm { get; }

        public VDS_NC SignData(VDS_NC_Data data, string certificatePem, string privateKeyPem)
        {
            if (data == null)
            {
                throw new Exception("Null VDS-NC Data cannot be signed");
            }
            
            var dataToBeSigned = GetDataToBeSigned(data);

            var x509 = X509Certificate2.CreateFromPem(certificatePem, privateKeyPem);
            var ecKey = x509.GetECDsaPrivateKey();

            var signature = ecKey.SignData(dataToBeSigned, this.HashAlgorithm);

            var sigBase64 = Base64Url.Encode(signature);

            var certBase64 = Base64Url.Encode(x509.RawData);

            var signatureZone = new SignatureZone
            {
                Algorithm = this.GetAlgorithm(),
                Certificate = certBase64,
                SignatureValue = sigBase64
            };

            return new VDS_NC
            {
                Data = data,
                SignatureZone = signatureZone
            };
        }

        public bool VerifySignature(VDS_NC vdsnc)
        {
            if (vdsnc == null)
            {
                throw new Exception("Null VDS-NC cannot be verified");
            }

            var signedData = GetDataToBeSigned(vdsnc.Data);

            var cert = vdsnc.SignatureZone.Certificate;
            var base64Cert = Base64Url.ConvertToBase64(cert);
            var certBytes = Convert.FromBase64String(base64Cert);
            var x509 = new X509Certificate2(certBytes);
            var publicKey = x509.GetECDsaPublicKey();

            var signature = vdsnc.SignatureZone.SignatureValue;
            var sigBytes = Base64Url.Decode(signature);

            return publicKey.VerifyData(signedData, sigBytes, this.HashAlgorithm);
        }

        private byte[] GetDataToBeSigned(VDS_NC_Data data)
        {
            var json = JsonConvert.SerializeObject(data);
            var canonicalisedJson = new JsonCanonicalizer(json).GetEncodedString();

            return Encoding.UTF8.GetBytes(canonicalisedJson);
        }

        private string GetAlgorithm()
        {
            switch(this.HashAlgorithm.ToString())
            {
                case AlgorithmConstants.SHA256: return "ES256";
                case AlgorithmConstants.SHA384: return "ES384";
                case AlgorithmConstants.SHA512: return "ES512";
                default: return "ES256";
            }
        }
    }
}
