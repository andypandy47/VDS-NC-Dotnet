# VDS-NC-Dotnet

Dotnet implementation of [ICAO VDS-NC](https://www.icao.int/Security/FAL/TRIP/PublishingImages/Pages/Publications/Guidelines%20-%20VDS%20for%20Travel-Related%20Public%20Health%20Proofs.pdf)

Inspired by [vds-sdk.js](https://github.com/Path-Check/vds-sdk.js)

### Current Features

- Signing
- Verification
- Proof of Vaccination Model

### Missing

- Proof of Test Model
- Schema Validation
- Complete Unit Testing

## Usage

With private key pem:

    -----BEGIN PRIVATE KEY-----
    MIGHAgEAMBMGByqGSM49AgEGCCqGSM49AwEHBG0wawIBAQQgZgp3uylFeCIIXozbZkCkSNr4DcLDxplZ1ax/u7ndXqahRANCAARkJeqyO85
    dyR+UrQ5Ey8EdgLyf9NtsCrwORAj6T68/elL19aoISQDbzaNYJjdD77XdHtd+nFGTQVpB88wPTwgb
    -----END PRIVATE KEY-----

With public cert pem:

    -----BEGIN CERTIFICATE-----
    MIIBYDCCAQYCEQCAG8uscdLb0ppaneNN5sB7MAoGCCqGSM49BAMCMDIxIzAhBgNVBAMMGk5hdGlvbmFsIENTQ0Egb2YgRnJpZXNsYW5kMQs
    wCQYDVQQGEwJGUjAeFw0yMTA0MjcyMDQ3MDVaFw0yNjAzMTIyMDQ3MDVaMDYxJzAlBgNVBAMMHkRTQyBudW1iZXIgd29ya2VyIG9mIEZyaW
    VzbGFuZDELMAkGA1UEBhMCRlIwWTATBgcqhkjOPQIBBggqhkjOPQMBBwNCAARkJeqyO85dyR+UrQ5Ey8EdgLyf9NtsCrwORAj6T68/elL19
    aoISQDbzaNYJjdD77XdHtd+nFGTQVpB88wPTwgbMAoGCCqGSM49BAMCA0gAMEUCIQDvDacGFQO3tuATpoqf40CBv09nfglL3wh5wBwA1uA7
    lAIgZ4sOK2iaaTsFNqENAF7zi+d862ePRQ9Lwymr7XfwVm0=
    -----END CERTIFICATE-----

With payload:

    "data": {
      "hdr": {
        "t": "icao.vacc",
        "v": 1,
        "is": "UTO"
      },
      "msg": {
        "uvci": "U32870",
        "pid": {
          "n": "Smith Bill",
          "dob": "1990-01-02",
          "sex": "M",
          "i": "A1234567Z",
          "ai": "L4567890Z"
        },
        "ve": [
          {
            "des": "XM68M6",
            "nam": "Comirnaty",
            "dis": "RA01.0",
            "vd": [
              {
                "dvc": "2021-03-03",
                "seq": 1,
                "ctr": "UTO",
                "adm": "RIVM",
                "lot": "VC35679",
                "dvn": "2021-03-24"
              },
              {
                "dvc": "2021-03-24",
                "seq": 2,
                "ctr": "UTO",
                "adm": "RIVM",
                "lot": "VC87540"
              }
            ]
          }
        ]
      }
    }

Instantiate new VDS-NC-Service. SHA256 is used by default however a different hash algorithm can be passed if required.

    var vdsncService = new VDS_NC_Service();

Call "SignData()" to create a signature zone based on the private key and canonicalised JSON data

    var signedVDSNC = vdsncService.SignData(vdsData, certificatePem, privateKeyPem);

Signature can then be verified by calling "VerifySignature()"

    var verified = vdsncService.VerifySignature(signedVDSNC);
