export interface ICertification {
  fileName: string;
  contentType: string;
  base64Data: string;
  certificationAssociation: ICertificationAssociation;
}
export interface ICertificationAssociation {
  title: string;
  issuingOrganization: string;
  description:string;
}