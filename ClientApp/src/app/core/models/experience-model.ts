export interface IExperience{
    jobTitle : string;
    company : string;
    duration : string;
    experienceAssociations : IExperienceResponsibilitiesAssociation[];
}
export interface IExperienceResponsibilitiesAssociation {
    experienceId: number;
    responsibilities: string;
    activeStatus : boolean;
  }