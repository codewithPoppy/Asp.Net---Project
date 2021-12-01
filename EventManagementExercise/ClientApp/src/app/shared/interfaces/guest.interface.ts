import { Allergy } from "./allergy.interface";

export interface Guest {
  id?: number;
  firstName: string;
  lastName: string;
  email: string;
  dob?: Date | string;
  allergies?: Allergy[]
}