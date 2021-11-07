using System;
using System.Linq;
using System.Security.Cryptography;
using Xunit;

namespace VDS_NC_Dotnet.Tests
{
    public class VDS_NC_ServiceTests
    {
        [Theory]
        [InlineData("SHA1")]
        [InlineData("MD5")]
        [InlineData(" ")]
        public void Constructor_UnsupportedAlgorithm_ThrowsException(string algorithmName)
        {
            Assert.Throws<Exception>(() => this.GetSut(new HashAlgorithmName(algorithmName)));
        }

        [Fact]
        public void SignData_NullData_ThrowsException()
        {
            var sut = this.GetSut();

            Assert.Throws<Exception>(() => sut.SignData(null, "", ""));
        }

        [Theory]
        [InlineData("SHA256", "ES256")]
        [InlineData("SHA384", "ES384")]
        [InlineData("SHA512", "ES512")]
        public void SignData_ValidData_ReturnsVDSNCWithSignatureZoneWithCorrectAlgorithm(string algorithmName, string expected)
        {
            var hashAlgo = new HashAlgorithmName(algorithmName);
            var sut = this.GetSut(hashAlgo);

            var vdsData = this.VDSExampleRepository.GetExampleData().First().Data;

            var signedVds = sut.SignData(vdsData, this.PublicCert, this.Pkcs8PrivateKey);

            Assert.NotNull(signedVds.SignatureZone);
            Assert.Equal(expected, signedVds.SignatureZone.Algorithm);
        }

        [Fact]
        public void VerifySignature_NullVDSNC_ThrowException()
        {
            var sut = this.GetSut();

            Assert.Throws<Exception>(() => sut.VerifySignature(null));
        }

        private VDS_NC_Service GetSut(HashAlgorithmName? hashAlgorithmName = null)
        {
            return new VDS_NC_Service(hashAlgorithmName);
        }

        private VDSNCExampleRepository VDSExampleRepository { get; } = new VDSNCExampleRepository();

        private readonly string Pkcs8PrivateKey = @"-----BEGIN PRIVATE KEY-----
                                                    MIGHAgEAMBMGByqGSM49AgEGCCqGSM49AwEHBG0wawIBAQQgZgp3uylFeCIIXozbZkCkSNr4DcLDxplZ1ax/u7ndXqahRANCAARkJeqyO85dyR+UrQ5Ey8EdgLyf9NtsCrwORAj6T68/elL19aoISQDbzaNYJjdD77XdHtd+nFGTQVpB88wPTwgb
                                                    -----END PRIVATE KEY-----";

        private readonly string PublicCert = @"-----BEGIN CERTIFICATE-----
                                               MIIBYDCCAQYCEQCAG8uscdLb0ppaneNN5sB7MAoGCCqGSM49BAMCMDIxIzAhBgNVBAMMGk5hdGlvbmFsIENTQ0Egb2YgRnJpZXNsYW5kMQswCQYDVQQGEwJGUjAeFw0yMTA0MjcyMDQ3MDVaFw0yNjAzMTIyMDQ3MDVaMDYxJzAlBgNVBAMMHkRTQyBudW1iZXIgd29ya2VyIG9mIEZyaWVzbGFuZDELMAkGA1UEBhMCRlIwWTATBgcqhkjOPQIBBggqhkjOPQMBBwNCAARkJeqyO85dyR+UrQ5Ey8EdgLyf9NtsCrwORAj6T68/elL19aoISQDbzaNYJjdD77XdHtd+nFGTQVpB88wPTwgbMAoGCCqGSM49BAMCA0gAMEUCIQDvDacGFQO3tuATpoqf40CBv09nfglL3wh5wBwA1uA7lAIgZ4sOK2iaaTsFNqENAF7zi+d862ePRQ9Lwymr7XfwVm0=
                                               -----END CERTIFICATE-----";
    }
}
