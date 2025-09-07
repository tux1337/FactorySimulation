# FactorySimulation
Simulation of a Factory with continous S3 Bucket access to proof an PAM Implementation with OIDC for S3 Authentication.

## Components
For a working environment the following components needs to be setup and the settings needs to be set in the respective classes of the CoreLib Library.
- OIDC with Auth0 (S3Wrapper.cs)
- PAM with HashiCorp Vault (Vault.cs)
- S3 implementation tested with MinIO and NetApp ONTAP S3, should be compatible with every S3 Object Storage. Credentials needs to be stored in HashiCorp Vault.

## NuGet Packages
- Auth0.AuthenticationApi Version 7.36.0
- AWSSDK.S3 Version 3.7.416.12 
- VaultSharp Version 1.7.0
