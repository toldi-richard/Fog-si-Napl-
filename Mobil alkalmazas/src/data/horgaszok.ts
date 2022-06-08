export interface Fisherman
{
  azonosito: number;
  szerepkorID: number;
  jelszo: string;
  email_cim: string;
}

export interface Catch
{
  fogasID: number;
  azonosito: number;
  helyszinID: number;
  horgaszhely: string;
  datum_idopont: Date;
  halfaj: string;
  suly: number;
}

export interface Helyszin {
  helyszinID: number;
  vizterulet_neve: string;
  vizterkod: number;
  
}