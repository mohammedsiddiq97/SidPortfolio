export interface IProjectDetail {
    fileName: string;
    contentType: string;
    base64Data: string;
    projectDetailAssociation: IProjectDetailAssociation;
  }
  export interface IProjectDetailAssociation {
    title: string;
    description:string;
    gitHubLink : string;
  }