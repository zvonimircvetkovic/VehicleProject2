import { Model } from './Model';

export interface Make {
  id?: number;
  name: string;
  abrv: string;
  models?: Model[];
}
