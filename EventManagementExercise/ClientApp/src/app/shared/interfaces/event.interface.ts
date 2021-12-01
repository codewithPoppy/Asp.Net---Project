import { Guest } from "./guest.interface";

export interface Event {
  id?: number;
  name: string;
  date: Date;
  guests?: Guest[] | number[];
}